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

        public override ResolveResultWithInfo ResolveWithoutCache()
        {
            var file = _owner.GetContainingFile();
            if (file == null) return ResolveResultWithInfo.Unresolved;

            var root = _owner.Root();
            
            foreach (var child in root.Descendants())
            {
                if (child is IDeclaration declaration && declaration.DeclaredName == GetName())
                {
                    return new ResolveResultWithInfo(new SimpleResolveResult(declaration.DeclaredElement), ResolveErrorType.OK);
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