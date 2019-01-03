using PsApp.Events;
using PsApp.Payloads;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
namespace PsApp
{
    /// <summary>
    /// class to help handle general manipulation of World_Events
    /// </summary>
    public class EventsHelper
    {
        public Events.World.Event[] _events = new Events.World.EventDataclass().GetEvents();

        /// <summary>
        /// returns a CompactEventPayload equivalent to the passed World_Event; useful for getting the name of an event
        /// </summary>
        /// <param name="world_Event"></param>
        /// <returns></returns>
        public Events.World.Event MatchEvents(World_Event world_Event)
        {
            Events.World.Event localCheck = null;
            for (int i = 0; i < _events.Length; i++)
            {
                if (world_Event.metagame_event_id == _events[i].event_id.ToString())
                {
                    localCheck = _events[i];
                    break;
                }
            }
            if (localCheck != null)
            {
                return localCheck;
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// DEPRECATED
        /// <para>Populates the ListView with events from a List<World_Events></para>
        /// </summary>
        /// <param name="n">List of World_Event to add </param>
        public List<FrontpagePayload> RemoveUselessMessages(List<World_Event> n)
        {
            List<FrontpagePayload> filteredList = new List<FrontpagePayload>();
            foreach (var i in n)
            {
                FrontpagePayload passMe = new FrontpagePayload();
                var anEvent = MatchEvents(i);
                if (anEvent != null)
                {
                    if (anEvent.event_name.Contains("Warpgate"))
                    {
                        //passMe = new Payloads.FrontpageMetaPayload()
                        //{
                        //    metagame_event_id = i.metagame_event_id,
                        //    timestamp = i.timestamp,
                        //    eventName = anEvent.event_name,
                        //    metagame_event_state_name = i.metagame_event_state_name,
                        //    continent = anEvent.continent,
                        //    event_type = i.event_type,
                        //    world_id_int = int.Parse(i.world_id),
                        //    world_id = i.world_id
                        //};
                        if (i.metagame_event_state_name == "started")
                        {
                            passMe = new Payloads.FrontpageWarpgateStartPayload()
                            {
                                metagame_event_id = i.metagame_event_id,
                                timestamp = i.timestamp,
                                eventName = $"{anEvent.continent} Warpgates",
                                continent = anEvent.continent,
                                event_type = i.event_type,
                                world_id_int = int.Parse(i.world_id),
                                world_id = i.world_id
                            };
                        }
                        if (i.metagame_event_state_name == "ended")
                        {
                            passMe = new Payloads.FrontpageWarpgateEndPayload()
                            {
                                metagame_event_id = i.metagame_event_id,
                                timestamp = i.timestamp,
                                eventName = $"{anEvent.continent} Warpgates",
                                continent = anEvent.continent,
                                event_type = i.event_type,
                                world_id_int = int.Parse(i.world_id),
                                world_id = i.world_id
                            };
                        }
                    }

                    //must be an alert
                    else if (anEvent.event_name.Contains(anEvent.continent))
                    {
                        passMe = new Payloads.FrontpageContPayload()
                        {
                            metagame_event_id = i.metagame_event_id,
                            timestamp = i.timestamp,
                            eventName = anEvent.event_name,
                            metagame_event_state_name = i.metagame_event_state_name,
                            continent = anEvent.continent,
                            event_type = i.event_type,
                            world_id_int = int.Parse(i.world_id),
                            world_id = i.world_id,
                            faction_nc = float.Parse(i.faction_nc),
                            faction_vs = float.Parse(i.faction_vs),
                            faction_tr = float.Parse(i.faction_tr)

                        };

                    }

                    else if (anEvent.event_name.Contains("Aerial") && i.metagame_event_state_name == "started")
                    {
                        passMe = new Payloads.FrontpageMetaPayload()
                        {
                            continent = anEvent.continent,                                           /*passMe.continent = anEvent.continent;                            */
                            eventName = anEvent.event_name,                                          /*passMe.eventName = anEvent.event_name;                           */
                            timestamp = i.timestamp,                                                 /*passMe.metagame_event_id = i.metagame_event_id;                  */
                            world_id = i.world_id,                                                   /*passMe.instance_id = i.instance_id;*/
                            metagame_event_state_name = i.metagame_event_state_name,                     /*passMe.event_type = i.event_type;                                */
                            metagame_event_id = i.metagame_event_id,
                            instance_id = i.instance_id,
                            world_id_int = int.Parse(i.world_id),
                            event_type = i.event_type
                        };
                    }
                    else if (anEvent.event_name.Contains("Technological")) // && i.metagame_event_state_name == "ended")
                    {
                        passMe = new Payloads.FrontpageExtendedPayload()
                        {
                            metagame_event_id = i.metagame_event_id,
                            timestamp = i.timestamp,
                            //eventName = anEvent.event_name,
                            eventName = "Tech Adv.",
                            metagame_event_state_name = i.metagame_event_state_name,
                            continent = anEvent.continent,
                            event_type = i.event_type,
                            world_id_int = int.Parse(i.world_id),
                            world_id = i.world_id,
                            faction_nc = (int)float.Parse(i.faction_nc),
                            faction_vs = (int)float.Parse(i.faction_vs),
                            faction_tr = (int)float.Parse(i.faction_tr)
                        };
                    }
                    else if (anEvent.event_name.Contains("Power") || anEvent.event_name.Contains("Bio") || anEvent.event_name.Contains("Aerial") || anEvent.event_name.Contains("Gaining")) // && i.metagame_event_state_name == "ended")
                    {
                        passMe = new Payloads.FrontpageScoredPayload()
                        {
                            metagame_event_id = i.metagame_event_id,
                            timestamp = i.timestamp,
                            eventName = anEvent.event_name,
                            metagame_event_state_name = i.metagame_event_state_name,
                            continent = anEvent.continent,
                            event_type = i.event_type,
                            world_id_int = int.Parse(i.world_id),
                            world_id = i.world_id,
                            faction_nc = (int)float.Parse(i.faction_nc),
                            faction_vs = (int)float.Parse(i.faction_vs),
                            faction_tr = (int)float.Parse(i.faction_tr)
                        };
                    }
                    else
                    {
                        try
                        {
                            passMe = new Payloads.FrontpageScoredPayload()
                            {
                                world_id_int = int.Parse(i.world_id),
                                continent = anEvent.continent,                            /*passMe.continent = anEvent.continent;                          */
                                eventName = anEvent.event_name,                           /*passMe.eventName = anEvent.event_name;                         */
                                faction_nc = (int)float.Parse(i.faction_nc),                     /*passMe.faction_nc = (int)double.Parse(i.faction_nc);           */
                                faction_tr = (int)float.Parse(i.faction_tr),                     /*passMe.faction_tr = (int)double.Parse(i.faction_tr);           */
                                faction_vs = (int)float.Parse(i.faction_vs),                     /*passMe.faction_vs = (int)double.Parse(i.faction_vs);           */
                                timestamp = i.timestamp,                                  /*passMe.timestamp = i.timestamp;                                */
                                world_id = i.world_id,                                    /*passMe.world_id = i.world_id;                                  */
                                metagame_event_state_name = i.metagame_event_state_name,  /*passMe.metagame_event_id = i.metagame_event_id;                */
                                metagame_event_id = i.metagame_event_id,                  /*passMe.metagame_event_state_name = i.metagame_event_state_name;*/
                                instance_id = i.instance_id,                              /*passMe.instance_id = i.instance_id;*/
                                event_type = i.event_type
                            };
                        }
                        catch (Exception e)
                        {
                            passMe = new Payloads.DebugPayload()
                            {
                                message = "There was an error parsing event information." +
                                "\nPlease take a screenshot of this and send it to me on discord.\n" +
                                $"TS: {i.timestamp.ToString()}  ID: {i.metagame_event_id}"
                            };
                        }
                    }
                    filteredList.Add(passMe);
                }

                passMe = null;
                anEvent = null;

            }//end for-each loop
            return filteredList;
        }
    }
}
