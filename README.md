# GeneratorOperation
  
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
within the target the template is generated for. The `Patches` folder includes an example with  
operations to generate lamps for every chunk in the game, including chunks added by mods, and using  
their color to control lamp glow.  
  
MIT licensed. To use in a mod, just copy over the assembly.