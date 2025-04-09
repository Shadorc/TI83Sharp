﻿using TI83Sharp;
using static TI83SharpTests.TestsHelper;

namespace TI83SharpTests;

[TestClass]
public class ExprTests
{
    [TestMethod]
    public void TestExpr_ImplicitMult_Const()
    {
        var source = ":2(5-3)";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("4", logger.MessageOutput);
    }

    [TestMethod]
    public void TestExpr_ImplicitMult_Var()
    {
        var source =
            @"
            :2→A   
            :5→B   
            :3→C   
            :AB-C
             ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("7", logger.MessageOutput);

        source =
            @"
            :2→A   
            :5→B   
            :3→C   
            :A(B-C)
             ";
        Interpret(source, out logger, out _);
        Assert.AreEqual("4", logger.MessageOutput);
    }

    [TestMethod]
    public void TestExpr_ImplicitMult_VarAns()
    {
        var source =
            @"
            :2→A   
            :3→C   
            :5→D
            :AAns-C
             ";
        Interpret(source, out var logger, out _);
        Assert.AreEqual("7", logger.MessageOutput);

        source =
            @"
            :2→A   
            :3→C   
            :5→D
            :Ans(A-C)
             ";
        Interpret(source, out logger, out _);
        Assert.AreEqual("-5", logger.MessageOutput);
    }

    [TestMethod]
    public void TestExpr_ConditionOptimized()
    {
        /*
         * :If A=B
         * :C+2→C
         */
        var source =
            @"
            :1→A
            :1→B
            :3→C
            :C+2(A=B→C
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5, environment.Get<TiNumber>("C"));
    }

    [TestMethod]
    public void TestExpr_Noop()
    {
        var source =
            @"
            :
            ";
        Interpret(source, out _, out _);
    }

    [TestMethod]
    public void TestExpr_MinusSign()
    {
        var source =
            @"
            :1→C
            :−C→B  
            ";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)(-1), environment.Get<TiNumber>("B"));
    }

    [TestMethod]
    public void TestExpr_TenPower()
    {
        var source = ":5ᴇ3→A";
        Interpret(source, out _, out var environment);
        Assert.AreEqual((TiNumber)5000, environment.Get<TiNumber>("A"));

        source = ":5ᴇ-5→A";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)0.00005, environment.Get<TiNumber>("A"));

        source = ":ᴇ-3→A";
        Interpret(source, out _, out environment);
        Assert.AreEqual((TiNumber)0.001, environment.Get<TiNumber>("A"));
    }
}
