using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Knowledge.KIF.Converter.formatters;
using Knowledge.KIF.Converter.model;
using Knowledge.KIF.Converter.model.kif;
using Knowledge.KIF.Converter.util;
using Knowledge.KIF.Converter.converters;
using Knowledge.KIF.Converter.writers;
using Knowledge.KIF.Converter.dependencies.impl;
using Knowledge;

namespace Knowledge.KIF.Converter {
    class Tester {
        [STAThread]
        public static void Main(String[] args) {
            Console.WriteLine("start");
            ExpComp.Clear();
            Scanner scanner = new Scanner("D:\\temp\\1.exp");

            ExpComp.parser = new Parser(scanner);
            ExpComp.parser.Parse();

//            foreach (DataFrame frame in ExpComp.dataFrames.Values) {
//                Console.WriteLine(frame.ToString());
//            }
            IModelBuilder modelBuilder = ModelBuilderFactory.getInstance().createBuilder(ModelBuilderFactory.KIF);
            converters.Converter converter = new converters.Converter(modelBuilder);
            IModel model = converter.convert(ExpComp.dataFrames.Values);
            SimpleKifFormatter formatter = new SimpleKifFormatter();
            formatter.write(model, Console.Out);

 //           model.store(Console.Out);


/*
            string text = IOUtils.readText("d:\\test\\t.txt");
            Console.WriteLine(text);
            DependenciesManager dependenciesManager = new DependenciesManager("d:\\test\\t.txt");
            dependenciesManager.print();
*/

            Console.ReadKey();
        
        }
    }
}