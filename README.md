# R.Activities
Package of custom activities which allows UiPath to integrate with R. 

Available activities:

- __R Scope__: _Initialize the R Engine_
- __Run R Script__: _Run any script created in R (extension .r)_
> Converter
  - __Get Char Vector:__ _Converts the SymbolicExpression to RDotNet.CharVector_
  - __Get Data Frame:__ _Converts the SymbolicExpression to RDotNet.DataFrame_
  - __Get Dynamic Vector:__ _Converts the SymbolicExpression to RDotNet.DynamicVector_
  - __Get Integer Vector:__ _Converts the SymbolicExpression to RDotNet.IntegerVector_
  - __Get List:__ _Converts the SymbolicExpression to System.Collection.Generics.List<T>_


| <sup>Not yet compatible with R versions above 3.4.3.</sup> |
| --- |
_____________________


Future improvements:

<sub>- Compatibility with the most recent versions of R.</sub>

<sub>- More activities to translate the SymbolicExpression object which outputs from the Run Script activity.</sub>

<sub>- More activities to interact with R (install packages, etc.).</sub>

<sub>- A better file selection for the Run R Script activity, to display the file explorer for easier file browsing and also enable relative paths to be used.</sub>




Copyright 2019 Bruno Costa

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
