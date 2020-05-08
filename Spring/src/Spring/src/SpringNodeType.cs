using System;
using Antlr4.Runtime;
using JetBrains.Diagnostics;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.TreeBuilder;

namespace JetBrains.ReSharper.Plugins.Spring
{
    internal class SpringFileNodeType : CompositeNodeType
    {
        public static readonly SpringFileNodeType Instance = new SpringFileNodeType("Spring_FILE", 0);

        private SpringFileNodeType(string s, int index) : base(s, index)
        {
        }

        public override CompositeElement Create()
        {
            return new SpringNodeFile();
        }
    }

    internal class SpringErrorNodeType : CompositeNodeWithArgumentType
    {
        public static readonly SpringErrorNodeType Instance = new SpringErrorNodeType("Spring_ERROR", 0);

        private SpringErrorNodeType(string s, int index) : base(s, index)
        {
        }

        public class Message
        {
            public string Text { get; }
            public int Length { get; }

            public Message(string text, int length)
            {
                Length = length;
                Text = text;
            }
        }

        public override CompositeElement Create()
        {
            throw new InvalidOperationException("Argument needed for SpringErrorNodeType factory");
        }

        public override CompositeElement Create(object m)
        {
            var message = (Message) m;
            return new SpringNodeError(message.Text, message.Length);
        }
    }

    internal class SpringCompositeNodeType : CompositeNodeWithArgumentType
    {
        public static readonly SpringCompositeNodeType NodeTypeBlock =
            new SpringCompositeNodeType("Spring_BLOCK", 0, typeof(SpringNodeCompositeAntlr));

        public static readonly SpringCompositeNodeType NodeTypeFunction =
            new SpringCompositeNodeType("Spring_FUN", 1, typeof(SpringNodeCompositeAntlr));

        public static readonly SpringCompositeNodeType NodeTypeIdentifier =
            new SpringCompositeNodeType("Spring_IDENTIFIER", 2, typeof(SpringNodeIdentifier));

        public static readonly SpringCompositeNodeType NodeTypeIdentifierDeclaration =
            new SpringCompositeNodeType("Spring_IDENTIFIER_DECL", 3, typeof(SpringNodeIdentifierDeclaration));


        private readonly Type _nodeConstructor;

        private SpringCompositeNodeType(string s, int index, Type nodeConstructor) : base(s, index)
        {
            _nodeConstructor = nodeConstructor;
        }

        public override CompositeElement Create()
        {
            throw new InvalidOperationException("Argument needed for SpringCompositeNodeType factory");
        }

        public override CompositeElement Create(object o)
        {
            var antlrContext = (RuleContext) o;
            return (CompositeElement) Activator.CreateInstance(_nodeConstructor, antlrContext, this);
        }
    }
}