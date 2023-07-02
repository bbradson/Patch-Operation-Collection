// Copyright (c) 2023 bradson
// This Source Code Form is subject to the terms of the MIT license.
// If a copy of the license was not distributed with this file,
// You can obtain one at https://opensource.org/licenses/MIT/.

global using System.Xml;
global using JetBrains.Annotations;
global using Verse;
using System;
using System.Text;

namespace GeneratorOperation;

[PublicAPI]
public abstract class GeneratorBase : PatchOperationPathed
{
	public string? value;

	public string Xpath => xpath;

	public abstract string RootNodeName { get; }

	protected override bool ApplyWorker(XmlDocument xml)
	{
		if (value is null)
			return LogError("had a null value");

		var result = true;
		var matchingNodes = xml.SelectNodes(xpath);

		if (matchingNodes is null)
			return LogError("found no matches");

		var matchingNodesArray = CreateArray(matchingNodes);

		for (var i = 0; i < matchingNodesArray.Length; i++)
		{
			var matchingNode = matchingNodesArray[i];

			var valueNode = CreateXmlContainer(SubstituteVariables(matchingNode, value)).node;

			if (!ApplyResultingContainer(xml, matchingNode, valueNode))
				result = false;
		}

		return result;
	}

	protected virtual bool ApplyResultingContainer(XmlDocument xmlDocument, XmlNode matchNode, XmlNode generatedNode)
	{
		var parentNode = matchNode.ParentNode;
		if (parentNode is null)
		{
			Log.Error($"'{matchNode}' lacks parent node at\n{matchNode.OuterXml}");
			return false;
		}

		if (generatedNode.Name != RootNodeName)
			return ApplyResultingNode(xmlDocument, matchNode, generatedNode);

		var valueChildNodes = generatedNode.ChildNodes;
		var result = true;

		for (var i = 0; i < valueChildNodes.Count; i++)
		{
			if (!ApplyResultingNode(xmlDocument, matchNode, valueChildNodes[i]))
				result = false;
		}

		return result;
	}

	protected abstract bool ApplyResultingNode(XmlDocument xmlDocument, XmlNode matchNode, XmlNode valueChild);

	protected static XmlNode[] CreateArray(XmlNodeList matchingNodes)
	{
		var matchingNodesArray = new XmlNode[matchingNodes.Count];

		for (var i = matchingNodesArray.Length; i-- > 0;)
			matchingNodesArray[i] = matchingNodes[i];

		return matchingNodesArray;
	}

	protected bool LogError(string message)
	{
		Log.Error($"'{ToString()}' in '{sourceFile}' {message}");
		return false;
	}

	protected static string SubstituteVariables(XmlNode context, string xmlString)
	{
		var builder = new StringBuilder(xmlString);

		for (var b = builder.Length; b-- > 0;)
		{
			if (builder[b] != '}')
				continue;

			var variableEnd = b;

			while (b > 0 && builder[--b] != '{')
				;

			var variableStart = b;
			var variable = builder.ToString(variableStart + 1, variableEnd - variableStart - 1);

			XmlNode? matchNode = null;
			try
			{
				matchNode = context.SelectSingleNode(variable);
			}
			catch (Exception e)
			{
				Log.Error($"Error in xpath for variable {{{variable}}} in\n{builder}\n{e}");
			}

			if (matchNode is null)
			{
				Log.Error($"No match found for xpath {{{variable}}} in\n{builder}");
				continue;
			}

			builder.Remove(variableStart, variableEnd - variableStart + 1);
			builder.Insert(variableStart, matchNode.OuterXml);
		}

		return builder.ToString();
	}

	protected static XmlContainer CreateXmlContainer(string xmlString) => new() { node = GetNodeFromString(xmlString) };

	protected static XmlElement? GetNodeFromString(string xmlString)
	{
		var doc = new XmlDocument();
		doc.LoadXml(xmlString);
		return doc.DocumentElement;
	}
}