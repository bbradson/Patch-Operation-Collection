# Patch Operation Collection
  
Various patch operations to enable types of patches that would normally not be possible through xml alone  
  
Included are:  
  
  
### GeneratorOperationV2  
PatchOperations to generate defs and more patch operations.  
  
The two operations to do this are:  
```
GeneratorOperationV2.DefGenerator
```
which generates a `Def` from a template in its `<value>` for each match on its `<xpath>` and
```
GeneratorOperationV2.PatchGenerator
```
which generates a `PatchOperation` from a template in its `<value>` for each match on its `<xpath`.  
  
The template takes curly braces as placeholders to replace with the values pointed by their path,  
within the target the template is generated for. The `ExamplePatches` folder includes an example with  
operations to generate lamps for every chunk in the game, including chunks added by mods, and using  
their color to control lamp glow. CDATA is required to allow use of the special characters these  
operations look for in the value node. V2 differs from GeneratorOperation by allowing nested braces  
in the template and accepting direct strings or numbers returned by xpath functions like name(),  
instead of only full nodes.    
  
  
### LanguageOperation  
A patch operation to conditionally run other operations depending on the selected language.  
  
```
<Operation Class="LanguageOperation.Conditional">
	<debug>false</debug><!-- optional. Can be set to true to log patch behaviour -->

	<language>LANGUAGE_HERE</language>

	<match Class="PATCH_OPERATION_HERE">
		<!-- this runs when the specified language is active -->
	</match>

	<nomatch Class="PATCH_OPERATION_HERE">
		<!-- this runs when the specified language is not active -->
	</nomatch>
</Operation>
```
  
  
### CopyOperation
Patch operations to copy nodes that are the target of one xpath into destinations with a different  
xpath.  
```
Class="CopyOperation.Add"
Class="CopyOperation.Insert"
Class="CopyOperation.Replace"
Class="CopyOperation.Set"
Class="CopyOperation.TryAdd"
<xpath><!-- the context xpath that value and destination both get scoped to -->
<value><!-- the value xpath. It is not required to have both xpath and value defined. Only having
one of the two set works too -->
<destination><!-- the xpath for the destination node to copy into -->
<debug>
```
  
  
### DatabaseDump  
A patch operation to log the full contents of the game's loaded Defs xml at the exact point in time  
that the operation runs from.  
```
Class="DatabaseDump.Operation"
<!-- no additional arguments. This always logs full database contents. -->
```
  
  
### DefNameLink  
A patch operation to link a defName to a def of a different name, intended to handle removal of the  
def that had its name used as link.  
```
Class="DefNameLink.Operation"
<defName><!-- the name to have linked to another def -->
<targetDef><!-- the def to have fetched for any lookup using the previous name -->
<defType><!-- the type of the def, ThingDef by default -->
<generationPhase><!-- The loading step to establish links in. PreResolve by default. Other options
are PreDefGeneration and PostResolve -->
<debug>
```
  
  
### PatchOperationSet  
A patch operation to set a target node to a specified value, similar to PatchOperationReplace, or  
create the node if it's missing. The depth of node creation here goes up to the first filter predicate.  
A target of `a/b[predicate]/c/d/e` can have `c`, `d` and `e` created if they're missing, but not `a` or  
`b`.  
```
Class="PatchOperationSet.Operation"
<xpath>
<value>
<debug>
```
  
  
### PatchOperationTryAdd  
A patch operation to set a value for a target node only if it's missing, and not replace if the target  
is already there. Just like PatchOperationSet, the Add part of this is capable of adding multiple nodes  
up to the first filter predicate.  
```
Class="PatchOperationTryAdd.Operation"
<xpath>
<value>
<debug>
```
  
  
### PostInheritanceOperation  
A patch operation that runs another patch operation after the game's xml inheritance process. Any def  
with ParentName attribute will have contents of all its ancestors inserted into it at that point, while  
keeping the same ParentName.  
```
Class="PostInheritanceOperation.Patch"
<operation Class="PatchOperation"><!-- the other operation to perform post inheritance -->
<debug>
```
  
  
### SaveGameCompatibility  
A patch operation to perform conversions for Things on save loading. This can do reassignments between  
def, stuff and thingClass, from the values the Thing has gotten saved with to new values as set in the  
patch operation. All nodes are optional, as long as there is at least one valid pair from previous to  
new. Multiple values set in previous are treated with AND, meaning only defs satisfying all conditions  
get values reassigned.  
```
Class="SaveGameCompatibility.Operation"
<previousDefName>
<previousStuff>
<previousClassName>
<newDefName>
<newStuff>
<newClassName>
<debug>
```
  
  
Licensed for the public domain under Creative Commons CC0. To use in a mod, just copy over the assembly. No legal notices required, but a link to github sources is encouraged.