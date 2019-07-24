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

