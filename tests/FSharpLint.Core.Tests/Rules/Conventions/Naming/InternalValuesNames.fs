module FSharpLint.Core.Tests.Rules.Conventions.InternalValuesNames

open NUnit.Framework
open FSharpLint.Framework.Rules
open FSharpLint.Rules

let config =
    { NamingConfig.Naming = Some NamingCase.CamelCase
      Underscores = Some NamingUnderscores.AllowPrefix
      Prefix = None
      Suffix = None }
[<TestFixture>]
type TestConventionsInternalValuesNames() =
    inherit TestAstNodeRuleBase.TestAstNodeRuleBase(InternalValuesNames.rule config)

    [<Test>]
    member this.InternalVariableIsCamelCase() =
        this.Parse """
module Program
  let internal cat = 1"""

        this.AssertNoWarnings()

    [<Test>]
    member this.InternalVariableIsPascalCase() =
        this.Parse """
module Program
  let internal Cat = 1"""

        Assert.IsTrue(this.ErrorExistsAt(3,17))

    [<Test>]
    member this.PublicVariableIsNotRecorded() =
        this.Parse """
module Program
  let Cat = 1"""

        this.AssertNoWarnings()

    [<Test>]
    member this.PascalCaseLetBindingInTypeIsNotRecorded() =
        this.Parse """
module program
  let Cat() = ()"""

        this.AssertNoWarnings()
