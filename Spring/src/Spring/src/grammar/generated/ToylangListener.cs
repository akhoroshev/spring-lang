//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from /home/akhoroshev/Documents/spring-lang/Spring/src/Spring/src/grammar/Toylang.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="ToylangParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface IToylangListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFile([NotNull] ToylangParser.FileContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.file"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFile([NotNull] ToylangParser.FileContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] ToylangParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] ToylangParser.BlockContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.blockWithBraces"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlockWithBraces([NotNull] ToylangParser.BlockWithBracesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.blockWithBraces"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlockWithBraces([NotNull] ToylangParser.BlockWithBracesContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] ToylangParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] ToylangParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtComment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtComment([NotNull] ToylangParser.StmtCommentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtComment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtComment([NotNull] ToylangParser.StmtCommentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtFunction([NotNull] ToylangParser.StmtFunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtFunction([NotNull] ToylangParser.StmtFunctionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtVariable([NotNull] ToylangParser.StmtVariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtVariable([NotNull] ToylangParser.StmtVariableContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.functionParameterNames"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionParameterNames([NotNull] ToylangParser.FunctionParameterNamesContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.functionParameterNames"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionParameterNames([NotNull] ToylangParser.FunctionParameterNamesContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.functionParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionParameter([NotNull] ToylangParser.FunctionParameterContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.functionParameter"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionParameter([NotNull] ToylangParser.FunctionParameterContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtWhile"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtWhile([NotNull] ToylangParser.StmtWhileContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtWhile"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtWhile([NotNull] ToylangParser.StmtWhileContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtIf"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtIf([NotNull] ToylangParser.StmtIfContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtIf"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtIf([NotNull] ToylangParser.StmtIfContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtAssigment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtAssigment([NotNull] ToylangParser.StmtAssigmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtAssigment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtAssigment([NotNull] ToylangParser.StmtAssigmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtReturn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtReturn([NotNull] ToylangParser.StmtReturnContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtReturn"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtReturn([NotNull] ToylangParser.StmtReturnContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.stmtExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStmtExpression([NotNull] ToylangParser.StmtExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.stmtExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStmtExpression([NotNull] ToylangParser.StmtExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCall([NotNull] ToylangParser.FunctionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCall([NotNull] ToylangParser.FunctionCallContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.functionArguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionArguments([NotNull] ToylangParser.FunctionArgumentsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.functionArguments"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionArguments([NotNull] ToylangParser.FunctionArgumentsContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.binaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBinaryExpression([NotNull] ToylangParser.BinaryExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.binaryExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBinaryExpression([NotNull] ToylangParser.BinaryExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.logicalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLogicalExpression([NotNull] ToylangParser.LogicalExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.logicalExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLogicalExpression([NotNull] ToylangParser.LogicalExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.eqExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEqExpression([NotNull] ToylangParser.EqExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.eqExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEqExpression([NotNull] ToylangParser.EqExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.compareExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompareExpression([NotNull] ToylangParser.CompareExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.compareExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompareExpression([NotNull] ToylangParser.CompareExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.additionExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdditionExpression([NotNull] ToylangParser.AdditionExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.additionExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdditionExpression([NotNull] ToylangParser.AdditionExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.multiplyExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplyExpression([NotNull] ToylangParser.MultiplyExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.multiplyExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplyExpression([NotNull] ToylangParser.MultiplyExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.atomExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAtomExpression([NotNull] ToylangParser.AtomExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.atomExpression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAtomExpression([NotNull] ToylangParser.AtomExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIdentifier([NotNull] ToylangParser.IdentifierContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIdentifier([NotNull] ToylangParser.IdentifierContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="ToylangParser.integralLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIntegralLiteral([NotNull] ToylangParser.IntegralLiteralContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="ToylangParser.integralLiteral"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIntegralLiteral([NotNull] ToylangParser.IntegralLiteralContext context);
}
