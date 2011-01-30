using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.Editor.Model
{
    // TODO: this class should be replaced by class hierarchy
    class KnowledgeNode
    {
        int id;
        string name;
        // Holds reference to Anton's IR structure
        object data;

        public KnowledgeNode(int id, string name) {
            this.id = id;
            this.name = name;
        }

        public KnowledgeNode(int id, string name, object data)
        {
            this.id = id;
            this.name = name;
            this.data = data;
        }

        public int getID() {
            return id;
        }

        public string getName() {
            return name;
        }

        public object getData() {
            return data;
        }

        public static int EMPTY = 0; // e.g. user clicked on root node
        // Frame knowledge
        public static int FRAME_CLASS = 11;
        public static int FRAME_INSTANCE = 12;
        public static int FRAME_RULESET = 13;
        public static int SLOT_OWN = 14;
        public static int SLOT_INSTANCE = 15;
        // Ontology knowledge
        public static int CONCEPT = 21;
        public static int PROPERTY = 22;
        public static int INSTANCE = 23;

        // TODO: mask for Frame/Ontology events
    }
}
