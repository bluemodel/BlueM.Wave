# Adding a new analysis function

To add a new analysis function in Wave
1. write a new analysis function class that inherits from `Analysis`
1. add the new analysis function to the `AnalysisFactory`

## 1. Create a new analysis class

The new analysis class must inherit from `Analysis`. It receives a list of input time series and can produce text, values, a chart, a table and/or a list of result time series.

Example `Analysis\TestAnalysis.vb`:
[!code-vb[](../../source/Analysis/TestAnalysis.vb)]

## 2. Add the new analysis function to the `AnalysisFactory`

Add the new analysis function to the AnalysisFactory (file `Analysis\AnalysisFactory.vb`). The locations to edit are highlighted below and marked with `EDIT THIS`.

[!code-vb[](../../source/Analysis/AnalysisFactory.vb?highlight=42,82-83,146-147)]