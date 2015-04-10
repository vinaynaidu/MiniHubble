# KOMODO

Simple monitoring services.  
Can monitor http and daemon services.  
Result of a simple code golf.

### To Run

Clone the repo, open the solution in Visual Studio, run.

### Endpoints


**1. Status endpoint** 

*url:* `/home/status`
*method:* `GET`  

Gets status of currently monitored services

Sample response: 

	{  
	   "payload":[  
	      {  
	         "Name":"Google homepage",
	         "Status":"OK",
	         "LastSeen":68
	      },
	      {  
	         "Name":"Console app 1",
	         "Status":"OK",
	         "LastSeen":94
	      },
	      {  
	         "Name":"Healthunlocked Website",
	         "Status":"Alarm",
	         "LastSeen":138
	      }
	   ]
	}

**2. Update service endpoint**

*url:* /home/status?id={id}`  
*method:* `PUT`  
*note:* `id is required; type: string`

Updates the `last_contact` of the service given by `string` `id`


### Adding new services to monitor

Configuration file is located at `/Content/config.json` ([here](MiniHubble/Content/config.json)). Add a new entry in the format: 

	{
	    "id": "healthunlocked",
	    "type": "web",
	    "name": "Healthunlocked Website",
	    "alert": 100,
	    "url": "https://healthunlocked.com"
	}

and it should be picked up on next restart