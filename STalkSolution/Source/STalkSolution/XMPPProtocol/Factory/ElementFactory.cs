

using System;
using System.Collections;

using XMPPProtocol.Xml.Dom;

namespace XMPPProtocol.Factory
{
	/// <summary>
	/// Factory class that implements the factory pattern for builing our Elements.
	/// </summary>
	public class ElementFactory
	{		
		/// <summary>
		/// This Hashtable stores Mapping of protocol (tag/namespace) to the XMPPProtocol objects
		/// </summary>
		private static Hashtable m_table = new Hashtable();

		static ElementFactory()
		{
			AddElementType("iq",				Uri.CLIENT,					typeof(XMPPProtocol.Protocol.client.IQ));
			AddElementType("message",			Uri.CLIENT,					typeof(XMPPProtocol.Protocol.client.Message));
			AddElementType("presence",			Uri.CLIENT,					typeof(XMPPProtocol.Protocol.client.Presence));
			AddElementType("error",				Uri.CLIENT,					typeof(XMPPProtocol.Protocol.client.Error));
						
			AddElementType("agent",				Uri.IQ_AGENTS,				typeof(XMPPProtocol.Protocol.iq.agent.Agent));
			
			AddElementType("item",				Uri.IQ_ROSTER,				typeof(XMPPProtocol.Protocol.iq.roster.RosterItem));
			AddElementType("group",				Uri.IQ_ROSTER,				typeof(XMPPProtocol.Protocol.Base.Group));
			AddElementType("group",				Uri.X_ROSTERX,				typeof(XMPPProtocol.Protocol.Base.Group));

			AddElementType("item",				Uri.IQ_SEARCH,				typeof(XMPPProtocol.Protocol.iq.search.SearchItem));			
			
            // Stream stuff
			AddElementType("stream",			Uri.STREAM,					typeof(XMPPProtocol.Protocol.Stream));			
            AddElementType("error",				Uri.STREAM,					typeof(XMPPProtocol.Protocol.Error));
			
			AddElementType("query",				Uri.IQ_AUTH,				typeof(XMPPProtocol.Protocol.iq.auth.Auth));
			AddElementType("query",				Uri.IQ_AGENTS,				typeof(XMPPProtocol.Protocol.iq.agent.Agents));
			AddElementType("query",				Uri.IQ_ROSTER,				typeof(XMPPProtocol.Protocol.iq.roster.Roster));
			AddElementType("query",				Uri.IQ_LAST,				typeof(XMPPProtocol.Protocol.iq.last.Last));
            AddElementType("query",				Uri.IQ_VERSION,				typeof(XMPPProtocol.Protocol.iq.version.Version));
			AddElementType("query",				Uri.IQ_TIME,				typeof(XMPPProtocol.Protocol.iq.time.Time));
			AddElementType("query",				Uri.IQ_OOB,					typeof(XMPPProtocol.Protocol.iq.oob.Oob));
			AddElementType("query",				Uri.IQ_SEARCH,				typeof(XMPPProtocol.Protocol.iq.search.Search));
			AddElementType("query",				Uri.IQ_BROWSE,				typeof(XMPPProtocol.Protocol.iq.browse.Browse));
			AddElementType("query",				Uri.IQ_AVATAR,				typeof(XMPPProtocol.Protocol.iq.avatar.Avatar));
			AddElementType("query",				Uri.IQ_REGISTER,			typeof(XMPPProtocol.Protocol.iq.register.Register));
			AddElementType("query",				Uri.IQ_PRIVATE,				typeof(XMPPProtocol.Protocol.iq.@private.Private));
            
            // Privacy Lists
            AddElementType("query",             Uri.IQ_PRIVACY,             typeof(XMPPProtocol.Protocol.iq.privacy.Privacy));
            AddElementType("item",              Uri.IQ_PRIVACY,             typeof(XMPPProtocol.Protocol.iq.privacy.Item));
            AddElementType("list",              Uri.IQ_PRIVACY,             typeof(XMPPProtocol.Protocol.iq.privacy.List));
            AddElementType("active",            Uri.IQ_PRIVACY,             typeof(XMPPProtocol.Protocol.iq.privacy.Active));
            AddElementType("default",           Uri.IQ_PRIVACY,             typeof(XMPPProtocol.Protocol.iq.privacy.Default));
                        
			// Browse
			AddElementType("service",			Uri.IQ_BROWSE,				typeof(XMPPProtocol.Protocol.iq.browse.Service));
			AddElementType("item",				Uri.IQ_BROWSE,				typeof(XMPPProtocol.Protocol.iq.browse.BrowseItem));

			// Service Discovery			
			AddElementType("query",				Uri.DISCO_ITEMS,			typeof(XMPPProtocol.Protocol.iq.disco.DiscoItems));			
			AddElementType("query",				Uri.DISCO_INFO,				typeof(XMPPProtocol.Protocol.iq.disco.DiscoInfo));
			AddElementType("feature",			Uri.DISCO_INFO,			    typeof(XMPPProtocol.Protocol.iq.disco.DiscoFeature));
			AddElementType("identity",			Uri.DISCO_INFO,			    typeof(XMPPProtocol.Protocol.iq.disco.DiscoIdentity));			
			AddElementType("item",				Uri.DISCO_ITEMS,			typeof(XMPPProtocol.Protocol.iq.disco.DiscoItem));

			AddElementType("x",					Uri.X_DELAY,				typeof(XMPPProtocol.Protocol.x.Delay));
			AddElementType("x",					Uri.X_AVATAR,				typeof(XMPPProtocol.Protocol.x.Avatar));
			AddElementType("x",					Uri.X_CONFERENCE,			typeof(XMPPProtocol.Protocol.x.Conference));
            AddElementType("x",                 Uri.X_EVENT,                typeof(XMPPProtocol.Protocol.x.Event));
			
			//AddElementType("x",					Uri.STORAGE_AVATAR,	typeof(XMPPProtocol.Protocol.storage.Avatar));
			AddElementType("query",				Uri.STORAGE_AVATAR,			typeof(XMPPProtocol.Protocol.storage.Avatar));

			// XData Stuff
			AddElementType("x",					Uri.X_DATA,					typeof(XMPPProtocol.Protocol.x.data.Data));
			AddElementType("field",				Uri.X_DATA,					typeof(XMPPProtocol.Protocol.x.data.Field));
			AddElementType("option",			Uri.X_DATA,					typeof(XMPPProtocol.Protocol.x.data.Option));
			AddElementType("value",				Uri.X_DATA,					typeof(XMPPProtocol.Protocol.x.data.Value));
            AddElementType("reported",          Uri.X_DATA,                 typeof(XMPPProtocol.Protocol.x.data.Reported));
            AddElementType("item",              Uri.X_DATA,                 typeof(XMPPProtocol.Protocol.x.data.Item));
			
			AddElementType("features",			Uri.STREAM,					typeof(XMPPProtocol.Protocol.stream.Features));

			AddElementType("register",			Uri.FEATURE_IQ_REGISTER,	typeof(XMPPProtocol.Protocol.stream.feature.Register));
            AddElementType("compression",       Uri.FEATURE_COMPRESS,       typeof(XMPPProtocol.Protocol.stream.feature.compression.Compression));
            AddElementType("method",            Uri.FEATURE_COMPRESS,       typeof(XMPPProtocol.Protocol.stream.feature.compression.Method));

			AddElementType("bind",				Uri.BIND,					typeof(XMPPProtocol.Protocol.iq.bind.Bind));
			AddElementType("session",			Uri.SESSION,				typeof(XMPPProtocol.Protocol.iq.session.Session));
			
			// TLS stuff
			AddElementType("failure",			Uri.TLS,					typeof(XMPPProtocol.Protocol.tls.Failure));
			AddElementType("proceed",			Uri.TLS,					typeof(XMPPProtocol.Protocol.tls.Proceed));
			AddElementType("starttls",			Uri.TLS,					typeof(XMPPProtocol.Protocol.tls.StartTls));

			// SASL stuff
			AddElementType("mechanisms",		Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Mechanisms));
			AddElementType("mechanism",			Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Mechanism));			
			AddElementType("auth",				Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Auth));
			AddElementType("response",			Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Response));
			AddElementType("challenge",			Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Challenge));
            
            // TODO, this is a dirty hacks for the buggy BOSH Proxy
            // BEGIN
            AddElementType("challenge",         Uri.CLIENT,                 typeof(XMPPProtocol.Protocol.sasl.Challenge));
            AddElementType("success",           Uri.CLIENT,                 typeof(XMPPProtocol.Protocol.sasl.Success));
            // END

			AddElementType("failure",			Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Failure));
			AddElementType("abort",				Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Abort));
			AddElementType("success",			Uri.SASL,					typeof(XMPPProtocol.Protocol.sasl.Success));
            
			// Vcard stuff
			AddElementType("vCard",				Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Vcard));
            AddElementType("TEL",				Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Telephone));
			AddElementType("ORG",				Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Organization));
			AddElementType("N",					Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Name));
			AddElementType("EMAIL",				Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Email));			
			AddElementType("ADR",				Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Address));
#if !CF
			AddElementType("PHOTO",				Uri.VCARD,					typeof(XMPPProtocol.Protocol.iq.vcard.Photo));
#endif
            // Server stuff
            //AddElementType("stream",            Uri.SERVER,                 typeof(XMPPProtocol.Protocol.server.Stream));
            //AddElementType("message",           Uri.SERVER,                 typeof(XMPPProtocol.Protocol.server.Message));

			// Component stuff
			AddElementType("handshake",			Uri.ACCEPT,					typeof(XMPPProtocol.Protocol.component.Handshake));
			AddElementType("log",				Uri.ACCEPT,					typeof(XMPPProtocol.Protocol.component.Log));
			AddElementType("route",				Uri.ACCEPT,					typeof(XMPPProtocol.Protocol.component.Route));
			AddElementType("iq",				Uri.ACCEPT,					typeof(XMPPProtocol.Protocol.component.IQ));
            AddElementType("message",           Uri.ACCEPT,                 typeof(XMPPProtocol.Protocol.component.Message));
            AddElementType("presence",          Uri.ACCEPT,                 typeof(XMPPProtocol.Protocol.component.Presence));
            AddElementType("error",             Uri.ACCEPT,                 typeof(XMPPProtocol.Protocol.component.Error));

			//Extensions (JEPS)
			AddElementType("headers",			Uri.SHIM,					typeof(XMPPProtocol.Protocol.extensions.shim.Header));
			AddElementType("header",			Uri.SHIM,					typeof(XMPPProtocol.Protocol.extensions.shim.Headers));
			AddElementType("roster",			Uri.ROSTER_DELIMITER,		typeof(XMPPProtocol.Protocol.iq.roster.Delimiter));
			AddElementType("p",					Uri.PRIMARY,				typeof(XMPPProtocol.Protocol.extensions.primary.Primary));
            AddElementType("nick",              Uri.NICK,                   typeof(XMPPProtocol.Protocol.extensions.nickname.Nickname));

			AddElementType("item",				Uri.X_ROSTERX,				typeof(XMPPProtocol.Protocol.x.rosterx.RosterItem));
			AddElementType("x",					Uri.X_ROSTERX,				typeof(XMPPProtocol.Protocol.x.rosterx.RosterX));

            // Filetransfer stuff
			AddElementType("file",				Uri.SI_FILE_TRANSFER,		typeof(XMPPProtocol.Protocol.extensions.filetransfer.File));
			AddElementType("range",				Uri.SI_FILE_TRANSFER,		typeof(XMPPProtocol.Protocol.extensions.filetransfer.Range));

            // FeatureNeg
            AddElementType("feature",           Uri.FEATURE_NEG,            typeof(XMPPProtocol.Protocol.extensions.featureneg.FeatureNeg));

            // Bytestreams
            AddElementType("query",             Uri.BYTESTREAMS,            typeof(XMPPProtocol.Protocol.extensions.bytestreams.ByteStream));
            AddElementType("streamhost",        Uri.BYTESTREAMS,            typeof(XMPPProtocol.Protocol.extensions.bytestreams.StreamHost));
            AddElementType("streamhost-used",   Uri.BYTESTREAMS,            typeof(XMPPProtocol.Protocol.extensions.bytestreams.StreamHostUsed));
            AddElementType("activate",          Uri.BYTESTREAMS,            typeof(XMPPProtocol.Protocol.extensions.bytestreams.Activate));
            AddElementType("udpsuccess",        Uri.BYTESTREAMS,            typeof(XMPPProtocol.Protocol.extensions.bytestreams.UdpSuccess));
            

			AddElementType("si",				Uri.SI,						typeof(XMPPProtocol.Protocol.extensions.si.SI));
            
            AddElementType("html",              Uri.XHTML_IM,               typeof(XMPPProtocol.Protocol.extensions.html.Html));
            AddElementType("body",              Uri.XHTML,                  typeof(XMPPProtocol.Protocol.extensions.html.Body));
            
            AddElementType("compressed",        Uri.COMPRESS,               typeof(XMPPProtocol.Protocol.extensions.compression.Compressed));
            AddElementType("compress",          Uri.COMPRESS,               typeof(XMPPProtocol.Protocol.extensions.compression.Compress));
            AddElementType("failure",           Uri.COMPRESS,               typeof(XMPPProtocol.Protocol.extensions.compression.Failure));
                    
            // MUC (JEP-0045 Multi User Chat)
            AddElementType("x",                 Uri.MUC,                    typeof(XMPPProtocol.Protocol.x.muc.Muc));
            AddElementType("x",                 Uri.MUC_USER,               typeof(XMPPProtocol.Protocol.x.muc.User));
            AddElementType("item",              Uri.MUC_USER,               typeof(XMPPProtocol.Protocol.x.muc.Item));
            AddElementType("status",            Uri.MUC_USER,               typeof(XMPPProtocol.Protocol.x.muc.Status));
            AddElementType("invite",            Uri.MUC_USER,               typeof(XMPPProtocol.Protocol.x.muc.Invite));
            AddElementType("decline",           Uri.MUC_USER,               typeof(XMPPProtocol.Protocol.x.muc.Decline));
            AddElementType("actor",             Uri.MUC_USER,               typeof(XMPPProtocol.Protocol.x.muc.Actor));
            AddElementType("history",           Uri.MUC,                    typeof(XMPPProtocol.Protocol.x.muc.History));
            AddElementType("query",             Uri.MUC_ADMIN,              typeof(XMPPProtocol.Protocol.x.muc.iq.admin.Admin));
            AddElementType("item",              Uri.MUC_ADMIN,              typeof(XMPPProtocol.Protocol.x.muc.iq.admin.Item));
            AddElementType("query",             Uri.MUC_OWNER,              typeof(XMPPProtocol.Protocol.x.muc.iq.owner.Owner));
            AddElementType("destroy",           Uri.MUC_OWNER,              typeof(XMPPProtocol.Protocol.x.muc.Destroy));
            

            //Jabber RPC JEP 0009            
            AddElementType("query",             Uri.IQ_RPC,                 typeof(XMPPProtocol.Protocol.iq.rpc.Rpc));
            AddElementType("methodCall",        Uri.IQ_RPC,                 typeof(XMPPProtocol.Protocol.iq.rpc.MethodCall));
            AddElementType("methodResponse",    Uri.IQ_RPC,                 typeof(XMPPProtocol.Protocol.iq.rpc.MethodResponse));

            // Chatstates Jep-0085
            AddElementType("active",            Uri.CHATSTATES,             typeof(XMPPProtocol.Protocol.extensions.chatstates.Active));
            AddElementType("inactive",          Uri.CHATSTATES,             typeof(XMPPProtocol.Protocol.extensions.chatstates.Inactive));
            AddElementType("composing",         Uri.CHATSTATES,             typeof(XMPPProtocol.Protocol.extensions.chatstates.Composing));
            AddElementType("paused",            Uri.CHATSTATES,             typeof(XMPPProtocol.Protocol.extensions.chatstates.Paused));
            AddElementType("gone",              Uri.CHATSTATES,             typeof(XMPPProtocol.Protocol.extensions.chatstates.Gone));

            // Jivesoftware Extenstions
            AddElementType("phone-event",       Uri.JIVESOFTWARE_PHONE,     typeof(XMPPProtocol.Protocol.extensions.jivesoftware.phone.PhoneEvent));
            AddElementType("phone-action",      Uri.JIVESOFTWARE_PHONE,     typeof(XMPPProtocol.Protocol.extensions.jivesoftware.phone.PhoneAction));
            AddElementType("phone-status",      Uri.JIVESOFTWARE_PHONE,     typeof(XMPPProtocol.Protocol.extensions.jivesoftware.phone.PhoneStatus));

            // Jingle stuff is in heavy development, we commit this once the most changes on the Jeps are done            
            //AddElementType("jingle",            Uri.JINGLE,                 typeof(XMPPProtocol.Protocol.extensions.jingle.Jingle));
            //AddElementType("candidate",         Uri.JINGLE,                 typeof(XMPPProtocol.Protocol.extensions.jingle.Candidate));

            AddElementType("c",                 Uri.CAPS,                   typeof(XMPPProtocol.Protocol.extensions.caps.Capabilities));

            AddElementType("geoloc",            Uri.GEOLOC,                 typeof(XMPPProtocol.Protocol.extensions.geoloc.GeoLoc));

            // Xmpp Ping
            AddElementType("ping",              Uri.PING,                   typeof(XMPPProtocol.Protocol.extensions.ping.Ping));

            //Ad-Hock Commands
            AddElementType("command",           Uri.COMMANDS,               typeof(XMPPProtocol.Protocol.extensions.commands.Command));
            AddElementType("actions",           Uri.COMMANDS,               typeof(XMPPProtocol.Protocol.extensions.commands.Actions));
            AddElementType("note",              Uri.COMMANDS,               typeof(XMPPProtocol.Protocol.extensions.commands.Note));

            // **********
            // * PubSub *
            // **********
            // Owner namespace
            AddElementType("affiliate",         Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Affiliate));
            AddElementType("affiliates",        Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Affiliates));
            AddElementType("configure",         Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Configure));
            AddElementType("delete",            Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Delete));
            AddElementType("pending",           Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Pending));
            AddElementType("pubsub",            Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.PubSub));
            AddElementType("purge",             Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Purge));
            AddElementType("subscriber",        Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Subscriber));
            AddElementType("subscribers",       Uri.PUBSUB_OWNER,           typeof(XMPPProtocol.Protocol.extensions.pubsub.owner.Subscribers));

            // Event namespace
            AddElementType("delete",            Uri.PUBSUB_EVENT,           typeof(XMPPProtocol.Protocol.extensions.pubsub.@event.Delete));
            AddElementType("event",             Uri.PUBSUB_EVENT,           typeof(XMPPProtocol.Protocol.extensions.pubsub.@event.Event));
            AddElementType("item",              Uri.PUBSUB_EVENT,           typeof(XMPPProtocol.Protocol.extensions.pubsub.@event.Item));
            AddElementType("items",             Uri.PUBSUB_EVENT,           typeof(XMPPProtocol.Protocol.extensions.pubsub.@event.Items));
            AddElementType("purge",             Uri.PUBSUB_EVENT,           typeof(XMPPProtocol.Protocol.extensions.pubsub.@event.Purge));

            // Main Pubsub namespace
            AddElementType("affiliation",       Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Affiliation));
            AddElementType("affiliations",      Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Affiliations));
            AddElementType("configure",         Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Configure));
            AddElementType("create",            Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Create));
            AddElementType("configure",         Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Configure));
            AddElementType("item",              Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Item));
            AddElementType("items",             Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Items));
            AddElementType("options",           Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Options));
            AddElementType("publish",           Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Publish));
            AddElementType("pubsub",            Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.PubSub));
            AddElementType("retract",           Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Retract));
            AddElementType("subscribe",         Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Subscribe));
            AddElementType("subscribe-options", Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.SubscribeOptions));
            AddElementType("subscription",      Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Subscription));
            AddElementType("subscriptions",     Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Subscriptions));
            AddElementType("unsubscribe",       Uri.PUBSUB,                 typeof(XMPPProtocol.Protocol.extensions.pubsub.Unsubscribe));           

            // HTTP Binding XEP-0124
            AddElementType("body",              Uri.HTTP_BIND,              typeof(XMPPProtocol.Protocol.extensions.bosh.Body));

            // Message receipts XEP-0184
            AddElementType("received",          Uri.MSG_RECEIPT,            typeof(XMPPProtocol.Protocol.extensions.msgreceipts.Received));
            AddElementType("request",           Uri.MSG_RECEIPT,            typeof(XMPPProtocol.Protocol.extensions.msgreceipts.Request));

            // Bookmark storage XEP-0048         
            AddElementType("storage",           Uri.STORAGE_BOOKMARKS,      typeof(XMPPProtocol.Protocol.extensions.bookmarks.Storage));
            AddElementType("url",               Uri.STORAGE_BOOKMARKS,      typeof(XMPPProtocol.Protocol.extensions.bookmarks.Url));
            AddElementType("conference",        Uri.STORAGE_BOOKMARKS,      typeof(XMPPProtocol.Protocol.extensions.bookmarks.Conference));
            
            // XEP-0047: In-Band Bytestreams (IBB)
            AddElementType("open",              Uri.IBB,                    typeof(XMPPProtocol.Protocol.extensions.ibb.Open));
            AddElementType("data",              Uri.IBB,                    typeof(XMPPProtocol.Protocol.extensions.ibb.Data));
            AddElementType("close",             Uri.IBB,                    typeof(XMPPProtocol.Protocol.extensions.ibb.Close));
                    
            // XEP-0153: vCard-Based Avatars
            AddElementType("x",                 Uri.VCARD_UPDATE,           typeof(XMPPProtocol.Protocol.x.vcard_update.VcardUpdate));

            // AMP
            AddElementType("amp",               Uri.AMP,                    typeof(XMPPProtocol.Protocol.extensions.amp.Amp));
            AddElementType("rule",              Uri.AMP,                    typeof(XMPPProtocol.Protocol.extensions.amp.Rule));

		}		
		
		/// <summary>
		/// Adds new Element Types to the Hashtable
		/// Use this function also to register your own created Elements.
        /// If a element is already registered it gets overwritten. This behaviour is also useful if you you want to overwrite
        /// classes and add your own derived classes to the factory.
		/// </summary>
		/// <param name="tag">FQN</param>
		/// <param name="ns"></param>
		/// <param name="t"></param>
		public static void AddElementType(string tag, string ns, System.Type t)
		{
            ElementType et = new ElementType(tag, ns);
            string key = et.ToString();
            // added thread safety on a user request
            lock (m_table)
            {
                if (m_table.ContainsKey(key))
                    m_table[key] = t;
                else
                    m_table.Add(et.ToString(), t);
            }
		}
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="prefix"></param>
		/// <param name="tag"></param>
		/// <param name="ns"></param>
		/// <returns></returns>
		public static Element GetElement(string prefix, string tag, string ns)
		{
			if (ns == null)
				ns = "";

			ElementType et = new ElementType(tag, ns);			
			System.Type t = (System.Type) m_table[et.ToString()];

			Element ret;			
			if (t != null)
				ret = (Element) System.Activator.CreateInstance(t);				
			else
			    ret = new Element(tag);				
			
			ret.Prefix = prefix;

			if (ns!="")
				ret.Namespace = ns;
			
			return ret;
		}		
	}  
}