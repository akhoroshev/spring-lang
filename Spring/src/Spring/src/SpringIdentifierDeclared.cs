using System.Collections.Generic;
using System.Xml;
using Antlr4.Runtime.Misc;
using ICSharpCode.NRefactory;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util.DataStructures;

namespace JetBrains.ReSharper.Plugins.Spring
{
    public class SpringIdentifierDeclared : IDeclaredElement
    {
        private readonly SpringNodeIdentifierDeclaration _declaration;

        public SpringIdentifierDeclared(SpringNodeIdentifierDeclaration declaration)
        {
            _declaration = declaration;
        }

        public IPsiServices GetPsiServices() => _declaration.GetPsiServices();

        public IList<IDeclaration> GetDeclarations() => new List<IDeclaration> {_declaration};

        public IList<IDeclaration> GetDeclarationsIn(IPsiSourceFile sourceFile)
        {
            var file = _declaration.GetSourceFile();

            return file == null || !file.Equals(sourceFile)
                ? (IList<IDeclaration>) EmptyList<IDeclaration>.Instance
                : new ArrayList<IDeclaration> {_declaration};
        }

        public DeclaredElementType GetElementType() => CLRDeclaredElementType.METHOD;

        public XmlNode GetXMLDoc(bool inherit) => null;

        public XmlNode GetXMLDescriptionSummary(bool inherit) => null;

        public bool IsValid() => _declaration.IsValid();

        public bool IsSynthetic() => false;

        public HybridCollection<IPsiSourceFile> GetSourceFiles()
        {
            var file = _declaration.GetSourceFile();
            return file == null ? HybridCollection<IPsiSourceFile>.Empty : new HybridCollection<IPsiSourceFile>(file);
        }

        public bool HasDeclarationsIn(IPsiSourceFile sourceFile) => sourceFile.Equals(_declaration.GetSourceFile());

        public string ShortName => _declaration.DeclaredName;
        public bool CaseSensitiveName => true;
        public PsiLanguageType PresentationLanguage => SpringLanguage.Instance;
    }
}