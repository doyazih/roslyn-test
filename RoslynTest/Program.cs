using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoslynTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //new SyntaxAnalysisTest().RunTest();
            //new SyntaxAnalysisTest().TestGetVariableDefinition();
            //new RoslynCodeAnalysisTest().RunTest();

            new SampleMicroserviceServer1().Get();

            Console.ReadKey();
        }
    }
}
