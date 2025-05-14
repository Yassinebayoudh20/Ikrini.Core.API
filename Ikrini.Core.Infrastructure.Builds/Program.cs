// ---------------------------------------------------------------
//   Copyright © Yassine Bayoudh. All Rights Reserved. | Ikrini
// ---------------------------------------------------------------

using System.IO;
using ADotNet.Clients.Builders;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;

string buildScriptPath = "../../../../.github/workflows/dotnet.yml";

string directoryPath = Path.GetDirectoryName(buildScriptPath);

if (Directory.Exists(directoryPath) is false)
{
    Directory.CreateDirectory(directoryPath);
}

GitHubPipelineBuilder.CreateNewPipeline()
    .SetName("Ikrini Core API Build")
        .OnPush("master")
        .OnPullRequest("master")
            .AddJob("build", job => job
                  .WithName("Build")
                  .RunsOn(BuildMachines.Windows2022)
                  .AddCheckoutStep("Check Out")
                  .AddSetupDotNetStep(version: "8.0.14")
                  .AddRestoreStep()
                  .AddBuildStep()
                  .AddTestStep(command: "dotnet test --no-build --verbosity normal --filter FullyQualifiedName!~Integrations"))
.SaveToFile(buildScriptPath);