using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using Knowledge.KIF.Converter.model.impl;


namespace Knowledge.KIF.Converter.writers
{
    class WritersFactoryOld
    {
        private static WritersFactoryOld _self_;
        private Hashtable _writers;

        private WritersFactoryOld() {
            _writers = new Hashtable();
        }

        public static WritersFactoryOld getInstance() {
            if (_self_ == null)
                _self_ = new WritersFactoryOld();
            return _self_;
        }

        public AbstractWriterOld getWriter(Knowledge.KIF.Converter.model.IKnowledgeItem item) {
            AbstractWriterOld result = (AbstractWriterOld)_writers[item.GetType()];
            if (result == null) { 
                if (item is Frame)
                    result = new FrameWriter();
            }
            return result;
        }
    }
}
