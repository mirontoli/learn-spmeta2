# Challenge 001 - Calculated Site Column

## goal and scenario
Provision a calculated field in a content type that is added to a list.

##how far we've come
**Calculated columns are lost on the list when the formula contains parts that change in localized versions**. In plain text, it is not present in the list (though it is there in the content type) if you try to use formula =IF([SomeField]="hello",1) on a Swedish site. It works fine in an English site. The formula gets converted to =OM([SomeField]="hello";1) on a Swedish site (in other lcid it is different). Don't mind the formula, it is just the simplest test. 

![Swedish formula](swedish-formula.png?raw=true)

What works is:
- All formulas that don't change on localization
- Any formula (I suppose) on English sites (LCID=1033)


## implementation
1. Add a text column
2. Add a calculated column
3. Add a new content type
4. Add our two columns to that content type
5. Add a generic list
6. Add our content type to our list
