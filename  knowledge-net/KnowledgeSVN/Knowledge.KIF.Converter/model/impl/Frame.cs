using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.model.impl
{
    public class Frame: IKnowledgeItem {
        #region IKnowledgeItem Members
        private String _artifactId;
        private String _name;

        public virtual String getArtifactId()
        {
           return _artifactId;
        }

        public virtual String getName()
        {
            return _name;
        }

        public virtual void setArtifactId(String artifactId)
        {
            _artifactId = artifactId;
        }

        public virtual void setName(String name)
        {
            _name = name;
        }
        #endregion
    }
}
