// Written in 2024 by bradson
// To the extent possible under law, the author(s) have dedicated all
// copyright and related and neighboring rights to this software to the
// public domain worldwide. This software is distributed without any
// warranty. You should have received a copy of the CC0 Public Domain
// Dedication along with this software. If not, see
// http://creativecommons.org/publicdomain/zero/1.0/.

using System.Xml;
using JetBrains.Annotations;
using Verse;

namespace DatabaseDump;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class Operation : PatchOperation
{
	protected override bool ApplyWorker(XmlDocument xml)
	{
		Log.Message(xml.OuterXml);
		return true;
	}

	public override string ToString() => GetType().FullName!;
}