using System;
using System.Collections.Generic;
using System.Text;

namespace Knowledge.KIF.Converter.model
{
    interface IKnowledgeItem
    {
        String getArtifactId();
        String getName();

        void setArtifactId(String artifactId);
        void setName(String name);
    }
}
