# Updating All Items View 

## Scenario
Add two generic lists, update "All Items" list views. The first list contains Title and ID, the second list contains Title and Modified. The issue is that both have the last added listview (Title and Modfied)

![issue](built-in-views-001.png?raw=true)

## Solution
Use Inherit on listview and define listview separately to have a better code structure. More info on [SPMeta2 yammer network](https://www.yammer.com/spmeta2feedback/#/Threads/show?threadId=625092745)