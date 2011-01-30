using System;
using System.Collections;

using System.Collections.Generic;
using System.Text;

using Knowledge.KIF.Converter.model.kif;
using Knowledge.KIF.Converter.model.ontolingua;

namespace Knowledge.KIF.Converter.converters.impl.ontolingua {

    class OntolinguaConverter: IConverter {

        private static bool isInstanceFrame(DataFrame frame) {
            return frame.frameType == DataFrame.FrameTypes.instanceFrame;
        }

        private OntolinguaObject createClassFrame(DataFrame frame) {
            return null;
        }

        private OntolinguaObject createInstanceFrame(DataFrame frame) {
            return null;
        }

        private OntolinguaObject createFrame(DataFrame frame) {
            return isInstanceFrame(frame) ? createInstanceFrame(frame) : createClassFrame(frame);
        }

        private OntolinguaObject createOwnSlot(Slot slot) {
            return null;
        }

        public ICollection<Object> convert(ICollection source) {
            List<Object> result = new List<object>();
            result.Add("ONTOLINGUA CONVERTER");
            result.Add("=================================");

            foreach (DataFrame frame in ExpComp.dataFrames.Values) {
                result.Add(createFrame(frame));
            }

            return result; 
        }

    }

}
