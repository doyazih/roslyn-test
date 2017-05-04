using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;
using Microsoft.CodeAnalysis.FindSymbols;
using Microsoft.CodeAnalysis.MSBuild;

namespace RoslynTest
{
    public class SyntaxAnalysisTest
    {
        public async void TestGetVariableDefinition() {


            const string pathToSolution = @"..\..\..\RoslynTest.sln";
            const string projectName = "RoslynTest";

            // start Roslyn workspace
            MSBuildWorkspace workspace = MSBuildWorkspace.Create();

            // open solution we want to analyze
            Solution solution = workspace.OpenSolutionAsync(pathToSolution).Result;

            Project project = solution.Projects.Where(_ => _.Name == projectName).FirstOrDefault();

            Compilation compliation = project.GetCompilationAsync().Result;

            List<SyntaxTree> trees = compliation.SyntaxTrees.ToList();

            SemanticModel semanticModel = compliation.GetSemanticModel(trees[0]);

            var symbol = await SymbolFinder.FindSymbolAtPositionAsync(semanticModel, 407, workspace);


        }

        public void RunTest()
        {
            string fullPathName = @"..\..\SampleMicroserviceServer1.cs";

            String csfile = File.ReadAllText(fullPathName);

            SyntaxTree tree = CSharpSyntaxTree.ParseText(csfile);

            var root = (CompilationUnitSyntax)tree.GetRoot();


            root.ChildNodes().Where(_ => _.GetType().Equals(typeof(NamespaceDeclarationSyntax))).ToList().ForEach(namespaceNode => {


                Console.WriteLine(string.Format("Namespace: {0}", ((NamespaceDeclarationSyntax)namespaceNode).Name));

                namespaceNode.ChildNodes().Where(_ => _.GetType().Equals(typeof(ClassDeclarationSyntax))).ToList().ForEach(classNode => {


                    Console.WriteLine(string.Format("└Class: {0}", ((ClassDeclarationSyntax)classNode).Identifier.ValueText));

                    classNode.ChildNodes().Where(_ => _.GetType().Equals(typeof(MethodDeclarationSyntax))).ToList().ForEach(methodNode => {

                        Console.WriteLine(string.Format("  └Method: {0}", ((MethodDeclarationSyntax)methodNode).Identifier.ValueText));

                        methodNode.ChildNodes().Where(_ => _.GetType().Equals(typeof(ParameterListSyntax))).ToList().ForEach(parameterListNodes => {

                            Console.WriteLine(string.Format("    └ParameterList: {0}", parameterListNodes.GetText()));

                        });
                    });
                });
            });
        }
    }
}
