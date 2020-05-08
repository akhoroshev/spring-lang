using System.Collections.Generic;
using JetBrains.Annotations;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Resolve;
using JetBrains.ReSharper.Psi.Resolve;
using JetBrains.ReSharper.Psi.Tree;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringIdentifierReference : TreeReferenceBase<SpringNodeIdentifier>
    {
        private readonly SpringNodeIdentifier _owner;

        public SpringIdentifierReference([NotNull] SpringNodeIdentifier owner) : base(owner)
        {
            _owner = owner;
        }

        private static IEnumerable<ITreeNode> TreeTraverseBottomUpLeft(ITreeNode node)
        {
            var current = node;
            foreach (var parent in node.PathToRoot())
            {
                foreach (var item in parent.Children())
                {
                    yield return item;
                    if (item.Equals(current))
                        break;
                }

                current = parent;
            }
        }

        public override ResolveResultWithInfo ResolveWithoutCache()
        {
            var file = _owner.GetContainingFile();
            if (file == null) return ResolveResultWithInfo.Unresolved;
            

            foreach (var item in TreeTraverseBottomUpLeft(_owner))
            {
                if (item is IDeclaration declaration && declaration.DeclaredName == GetName())
                {
                    return new ResolveResultWithInfo(new SimpleResolveResult(declaration.DeclaredElement),
                        ResolveErrorType.OK);
                }
            }

            return ResolveResultWithInfo.Unresolved;
        }

        public override string GetName() => _owner.Name;

        public override ISymbolTable GetReferenceSymbolTable(bool useReferenceName)
        {
            throw new System.NotImplementedException();
        }

        public override TreeTextRange GetTreeTextRange() => _owner.GetTreeTextRange();

        public override IReference BindTo(IDeclaredElement element) => this;

        public override IReference BindTo(IDeclaredElement element, ISubstitution substitution) => this;

        public override IAccessContext GetAccessContext() => new DefaultAccessContext(_owner);

        public override bool IsValid() => _owner.IsValid();
    }
}