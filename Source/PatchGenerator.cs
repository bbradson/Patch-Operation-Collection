// Copyright (c) 2023 bradson
// This Source Code Form is subject to the terms of the MIT license.
// If a copy of the license was not distributed with this file,
// You can obtain one at https://opensource.org/licenses/MIT/.

namespace GeneratorOperation;

[UsedImplicitly]
public class PatchGenerator : GeneratorBase
{
	public override string RootNodeName => "Patch";

	protected override bool ApplyResultingNode(XmlDocument xmlDocument, XmlNode matchNode, XmlNode valueChild)
	{
		var patch = DirectXmlToObject.ObjectFromXml<PatchOperation>(valueChild, doPostLoad: false);
		patch.sourceFile = sourceFile;
		var result = patch.Apply(xmlDocument);

		if (!result)
		{
			Log.Error($"{patch} at file {patch.sourceFile} {
				(patch is PatchOperationPathed pathed ? $"with xpath '{pathed.xpath}' " : "")}failed.");
		}

		return result;
	}
}