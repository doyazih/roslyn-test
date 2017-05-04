using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTest
{
    public class RoslynCodeAnalysisTest
    {
        public void RunTest()
        { 
            const string pathToSolution = @"..\..\..\RoslynTest.sln";
            const string projectName = "RoslynTest";

            // start Roslyn workspace
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            // open solution we want to analyze
            Solution solutionToAnalyze = workspace.OpenSolutionAsync(pathToSolution).Result;

            Project project = solutionToAnalyze.Projects.ToList()[0];

            Compilation compliation = project.GetCompilationAsync().Result;


        }
    }
}
