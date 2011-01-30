/*
 * Created by: M. Sigalin
 * Created: Thursday, October 26, 2006
 */

using System;
using System.Collections;
using Knowledge.KIF.Converter.model;

namespace Knowledge.KIF.Converter.converters {
    
    //Director for IModelBuilder
    public class Converter {
        private IModelBuilder _builder;

        private static bool isInstanceFrame(DataFrame frame) {
            return frame.frameType == DataFrame.FrameTypes.instanceFrame;
        }
        
        public Converter(IModelBuilder builder) {
            if (builder == null)
                throw new ArgumentNullException("builder cann't be null");
            _builder = builder;
            _builder.setDirector(this);
        }
        
        public IModel convert(ICollection source) {
            foreach (DataFrame frame in ExpComp.dataFrames.Values) {
                if (isInstanceFrame(frame))
                    _builder.createInstanceFrame(frame);
                else
                    _builder.createClassFrame(frame);
            }
            return _builder.getModel();
        }
        
        
        private string findInParents(string slotName, DataFrame frame) {
            IList parents = frame.isA;
            foreach (string s in parents) {
                DataFrame parent = ExpComp.getDataFrame(s);
                if (parent.instanceSlots.Contains(slotName))
                    return s;
                string mbOwner = findInParents(slotName, parent);
                if (mbOwner != null)
                    return mbOwner;
            }
            return null;            
        }
        
        public string getSlotOwner(string slotName, DataFrame frame) {//TODO: refactoring
            string result = findInParents(slotName, frame);
/*
            if (frame.isA == null || frame.isA.Count == 0)
                return frame.iden;
*/
            return result == null ? frame.iden : result;
        }
    }
}