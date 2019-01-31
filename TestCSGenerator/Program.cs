using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestCSGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //string applicationDir = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).AbsolutePath) + "\\";

            CreateRunTimeTT();
            CretateCodeDOM();
        }

        static void CreateRunTimeTT()
        {
            var template = new RunTimeTT();
            template.Properties = new List<string> { "IntProp1", "IntProp2", "IntProp3" };
            var templateOutput = template.TransformText();
            File.WriteAllText("RunTimeTTOutput.cs", templateOutput);
        }

        static void CretateCodeDOM()
        {
            var compileUnit = new CodeCompileUnit();

            var codeDomNamespace = new CodeNamespace("CodeDOM");
            codeDomNamespace.Imports.Add(new CodeNamespaceImport("System"));
            compileUnit.Namespaces.Add(codeDomNamespace);

            var targetClass = new CodeTypeDeclaration("MyGeneratedClass");
            targetClass.IsClass = true;
            targetClass.TypeAttributes = TypeAttributes.Public;
            codeDomNamespace.Types.Add(targetClass);

            for (int i = 1; i < 4; i++)
            {
                var field = new CodeMemberField();
                field.Attributes = MemberAttributes.Private;
                field.Name = "IntProp" + i;
                field.Type = new CodeTypeReference(typeof(int));
                targetClass.Members.Add(field);
            }

            var provider = CodeDomProvider.CreateProvider("CSharp");
            var options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter("CodeDOMOutput.cs"))
            {
                provider.GenerateCodeFromCompileUnit(
                    compileUnit, sourceWriter, options);
            }
        }
    }
}
