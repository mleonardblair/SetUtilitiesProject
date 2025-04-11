# Array Utilities – Unit Testing & Debugging in C#

## 📚 Project Overview

This project demonstrates comprehensive unit testing, bug discovery, and test-driven debugging of a C# utility class that performs operations on integer sets. These operations include union, intersection, uniqueness checks, and subset evaluation.

The original implementation contained several logic errors, which were identified and corrected using a full suite of MSTest unit tests. Over 40 test cases were written to cover all logical paths, validate expected behavior, and handle edge cases.

## 🛠️ Tech Stack

- C#
- MSTest Framework
- Visual Studio
- .NET Framework

## ✅ Objectives

- Achieve 100% code coverage of all public methods.
- Validate correct behavior under normal, boundary, and invalid input conditions.
- Identify and document logic bugs through failed test cases.
- Apply test-driven development (TDD) principles and QA best practices.

## 🔍 Key Features

- 40+ MSTest unit tests covering:
  - Set creation with range and uniqueness constraints
  - Subset relationship logic
  - Union and intersection behavior
  - Input validation and exception handling
- Negative testing using `[ExpectedException]` for invalid parameters
- Edge case testing for empty sets, reversed bounds, and invalid sizes
- All tests complete execution in under 5 seconds

## 🐞 Bug Summary

Discovered bugs include:

- Subset detection logic failing with unordered inputs
- Off-by-one error causing values below minimum in generated sets
- Missing validation for set size constraints
- Incorrect conditional logic causing false positives in subset tests

Each bug is documented at the top of the unit test file with test cases and suggested fixes.

## 🧪 How to Run the Tests

1. Open `SetUtilitiesProject.sln` in Visual Studio.
2. Build the solution to restore dependencies.
3. Open the Test Explorer (Test > Test Explorer).
4. Click "Run All Tests" to execute the full test suite.

## 🚀 Author

Developed by: **M. Leonard Blair**  
GitHub: https://github.com/mleonardblair

> This repository is for educational and portfolio demonstration purposes only.
> The source code and tests were independently written for practice and skills development.

