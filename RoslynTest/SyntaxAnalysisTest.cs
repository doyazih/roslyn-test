using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.IO;

namespace RoslynTest
{
    public class SyntaxAnalysisTest
    {

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
