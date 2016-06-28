﻿using System.Collections.Generic;
using System.Text;

namespace Assets.Kanau.Utils {
    public class AFrameNode {
        List<AFrameNode> children = new List<AFrameNode>();
        public string NodeType { get; private set; }
        List<Attribute> attributes = new List<Attribute>();

        class Attribute {
            public string name;
            public IProperty property;
        }

        public AFrameNode(string nodetype) {
            this.NodeType = nodetype;
        }

        public void AddChild(AFrameNode node) {
            children.Add(node);
        }


        public void AddAttribute(string name, IProperty property) {
            var attr = new Attribute() { name = name, property = property };
            attributes.Add(attr);
        }
        public void AddAttribute(string name, string value) {
            AddAttribute(name, new SimpleProperty<string>(value));
        }

        public void BuildSource(StringBuilder sb) {
            sb.AppendLine("");

            var tagname = string.Format("<{0} ", NodeType);
            sb.Append(tagname);
            foreach(var attr in attributes) {
                var value = attr.property.MakeString();
                if(value.Length == 0) { continue; }
                var line = string.Format("{0}=\"{1}\"", attr.name, value);
                sb.Append(line);
                sb.Append(" ");
            }
            sb.Append(">");

            foreach(var child in children) {
                child.BuildSource(sb);
            }

            var endtag = string.Format("</{0}>", NodeType);
            sb.Append(endtag);
        }
    }
}
