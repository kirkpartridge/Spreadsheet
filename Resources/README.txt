Kirk Partridge
u0691288
11/04/2014

EXTRA FEATURES:  SUM function, GRAPHING abilities, HELP WEBPAGE

GUI is working well, although I wish I had time to figure out a quicker data
input implementation, such as being able to use arrow keys.  Was able to add the
cell selection changing upon clicking enter.  Tests are being weird. Graphing works as a bar graph, 
didn't have timeto look into scatter plot and others.

The spreadsheet application as a whole is pretty sound and thorough.

Sum Function: Functionality was added to the spreadsheet, the method "SetContentsOfCell" was slightly
modified in that if it detects the "sum" keyword, the cells are parsed out and entered as a formula,
for example "SUM(A1:A4)" is entered as "=A1+A2+A3+A4".