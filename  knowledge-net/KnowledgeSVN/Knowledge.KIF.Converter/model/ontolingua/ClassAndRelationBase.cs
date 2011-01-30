/*
 * Created by: M. Sigalin
 * Created: Monday, October 30, 2006
 */

using System;
using System.Collections.Generic;
using Knowledge.KIF.Converter.model.kif;

namespace Knowledge.KIF.Converter.model.ontolingua {
    public abstract class ClassAndRelationBase : IfDef_DefSentenceOwner {
        public static readonly string CONSTRAINTS_SENTENCE = ":CONSTRAINTS";
        public static readonly string EQUIVALENT_SENTENCE = ":EQUIVALENT";
        public static readonly string SUFFICIENT_SENTENCE = ":SUFFICIENT";
        public static readonly string DEFAULT_CONSTRAINTS_SENTENCE = ":DEFAULT-CONSTRAINTS";
        public static readonly string AXIOM_DEF_SENTENCE = ":AXIOM-DEF";
        public static readonly string AXIOM_CONSTRAINTS_SENTENCE = ":AXIOM-CONSTRAINTS";
        public static readonly string AXIOM_DEFAULTS_SENTENCES = ":AXIOM-DEFAULTS";
        public static readonly string THEORY = ":THEORY";
        public static readonly string IMPLEMENTATION = ":IMPLEMENTATION";
        public static readonly string CLASS_SLOTS_SPECS = ":CLASS-SLOTS";
        public static readonly string INSTANCE_SLOTS_SPECS = ":INSTANCE-SLOTS";
        public static readonly string DEFAULT_SLOT_VALUES_SPECS = ":DEFAULT-SLOT-VALUES";
//    public static readonly string[:ISSUES <issue-tree>])

        private OntolinguaNamedSentence _constraints;
        private OntolinguaNamedSentence _equvalent;
        private OntolinguaNamedSentence _sufficient;
        private OntolinguaNamedSentence _defaultConstraints;
        private OntolinguaNamedSentence _axiomDef;
        private OntolinguaNamedSentence _axiomConstraints;
        private OntolinguaNamedSequenceOfSentences _axiomDefaults;

        private OntolinguaNamedSequenceOfSentences _classSlots;
        private OntolinguaNamedSequenceOfSentences _instanceSlots;
        private OntolinguaNamedSequenceOfSentences _defaultSlotValues;

        public KifSentence Constraints {
            get { return _constraints.Sentence; }
            set { _constraints = new OntolinguaNamedSentence(CONSTRAINTS_SENTENCE, value);  }
        }

        public KifSentence Equvalent {
            get { return _equvalent.Sentence; }
            set { _equvalent = new OntolinguaNamedSentence(EQUIVALENT_SENTENCE, value); }
        }

        public KifSentence Sufficient {
            get { return _sufficient.Sentence; }
            set { _sufficient = new OntolinguaNamedSentence(SUFFICIENT_SENTENCE, value); }
        }

        public KifSentence DefaultConstraints {
            get { return _defaultConstraints.Sentence; }
            set { _defaultConstraints = new OntolinguaNamedSentence(DEFAULT_CONSTRAINTS_SENTENCE, value); }
        }

        public KifSentence AxiomDef {
            get { return _axiomDef.Sentence; }
            set { _axiomDef = new OntolinguaNamedSentence(AXIOM_DEF_SENTENCE, value); }
        }

        public KifSentence AxiomConstraints {
            get { return _axiomConstraints.Sentence; }
            set { _axiomConstraints = new OntolinguaNamedSentence(AXIOM_CONSTRAINTS_SENTENCE, value); }
        }

        public KifSequence<KifSentence> axiomDefaults {//TODO:
            get { return _axiomDefaults.Sentences; }
            set { _axiomDefaults = new OntolinguaNamedSequenceOfSentences(AXIOM_DEFAULTS_SENTENCES, value); }
        }

        public KifSequence<KifSentence> classSlots {//TODO:
            get { return _classSlots.Sentences; }
            set { _classSlots = new OntolinguaNamedSequenceOfSentences(CLASS_SLOTS_SPECS, value); }
        }

        public KifSequence<KifSentence> instanceSlots {//TODO:
            get { return _instanceSlots.Sentences; }
            set { _instanceSlots = new OntolinguaNamedSequenceOfSentences(INSTANCE_SLOTS_SPECS, value); }
        }

        public KifSequence<KifSentence> defaultSlotValues {//TODO:
            get { return _defaultSlotValues.Sentences; }
            set { _defaultSlotValues = new OntolinguaNamedSequenceOfSentences(DEFAULT_SLOT_VALUES_SPECS, value); }
        }

        public ClassAndRelationBase(string name, Comment comment, KifSequence<KifIndividualVariable> argList)
            : base(name, comment, checkArg(argList)) {
        }

        private static KifSequence<KifIndividualVariable> checkArg(KifSequence<KifIndividualVariable> argList) {
            if (argList == null || argList.isEmpty())
                throw new ArgumentNullException("class argument cann't be null or empty");//TODO:
            return new KifSequence<KifIndividualVariable>(argList);
        }
        
        protected override string getBody() {
            return "";
        }

        protected override ICollection<IModelItem> getInnerChildren() {
            ICollection<IModelItem> list = base.getInnerChildren();
            addNotNullItem(list, _constraints);
            addNotNullItem(list, _equvalent);
            addNotNullItem(list, _sufficient);
            addNotNullItem(list, _defaultConstraints);
            addNotNullItem(list, _axiomDef);
            addNotNullItem(list, _axiomConstraints);
            addNotNullItem(list, _axiomDefaults);
            addNotNullItem(list, _classSlots);
            addNotNullItem(list, _instanceSlots);
            addNotNullItem(list, _defaultSlotValues);
            return list;
        }      
        
        protected void addNotNullItem(ICollection<IModelItem> list, IModelItem item) {
            if (item != null)
                list.Add(item);           
        }
    }
}