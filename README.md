# Patch Operation Collection
  
Various patch operations to enable types of patches that would normally not be possible through xml alone  
  
Included are:  
  
  
GeneratorOperation  
PatchOperations to generate defs and more patch operations.  
  
The two operations to do this are:  
```
GeneratorOperation.DefGenerator
```
which generates a `Def` from a template in its `<value>` for each match on its `<xpath>` and
```
GeneratorOperation.PatchGenerator
```
which generates a `PatchOperation` from a template in its `<value>` for each match on its `<xpath`.  
  
The template takes curly braces as placeholders to replace with the values pointed by their path,  
within the target the template is generated for. The `ExamplePatches` folder includes an example with  
operations to generate lamps for every chunk in the game, including chunks added by mods, and using  
their color to control lamp glow. CDATA is required to allow use of the special characters these  
operations look for in the value node.  
  
  
LanguageOperation  
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
  
Licensed for the public domain under Creative Commons CC0. To use in a mod, just copy over the assembly. No legal notices required, but a link to github sources is encouraged.