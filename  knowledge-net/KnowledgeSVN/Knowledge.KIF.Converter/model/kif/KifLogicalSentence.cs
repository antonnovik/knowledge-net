/*
 * Created by: M. Sigalin
 * Created: Tuesday, October 24, 2006
 */

using System;
using System.Collections.Generic;

namespace Knowledge.KIF.Converter.model.kif {
    public abstract class KifLogicalSentence: KifSentence {
        protected abstract string getName();

        protected abstract KifSequence<KifSentence> getSequence();

        public override String ToString() {
            return (LEFT_BRACKET + getName() + SPACE + getSequence().ToString() + RIGHT_BRACKET);
        }

        public override bool isLeaf() {
            return false;
        }

        public override IList<IModelItem> getChildren() {
            List<IModelItem> list = new List<IModelItem>();
            list.Add(LexemFactory.getInstance().getLexem(LEFT_BRACKET));
            list.Add(new KifConstant(getName()));
            list.Add(DELIMITER);
            list.AddRange(getSequence().getChildren());
            list.Add(LexemFactory.getInstance().getLexem(RIGHT_BRACKET));
            return list;
        }
    }
}