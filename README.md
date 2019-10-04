[![Build status](https://ci.appveyor.com/api/projects/status/kr3gtvor521ab2tw?svg=true)](https://ci.appveyor.com/project/SurajGupta/obeautifulcode-assertion)

[![Nuget status](https://img.shields.io/nuget/v/OBeautifulCode.Assertion.Recipes.Must.svg)](https://www.nuget.org/packages/OBeautifulCode.Assertion.Recipes.Must)  OBeautifulCode.Assertion.Recipes.Must

# OBeautifulCode.Assertion
==========================

Terminology

Subject - The thing you are performing checks on.
Subject Name - What you name the subject.
Verification - The check(s) you are performing.  For example, BeNotNull() is a verification.
Assertion Kind - The kind or flavor of assertion:  Test, Argument, or Operation
Assertion - The subject, assertion kind, and verifications.  For example:  As a Test assertion, I assert that the subject X, named "MyValue" is greater than 5 (verification).