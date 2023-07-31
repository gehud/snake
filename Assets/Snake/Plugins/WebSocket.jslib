var WebSocketLibrary = {
	$WebSocketState: {
        instance: null
    },

    WebSocketOpen: function(url) {
        var urlStr = UTF8ToString(url);
        WebSocketState.instance = new WebSocket(urlStr);

        WebSocketState.instance.onopen = function(event) {
            console.log("[WebSocket]: Opened");
        };

        WebSocketState.instance.onmessage = function(event) {
            console.log("[WebSocket]:", event.data);
            var obj = JSON.parse(event.data);
            if (obj.type === "game-created") {
                console.log("[WebSocket]: Game created");
                unityInstance.SendMessage("WebSocket", "CreateGame", obj.payload.id);
            }
        };

        WebSocketState.instance.onclose = function(event) {
            if (event.wasClean) {
                console.log("[WebSocket]: Closed");
            } else {
                console.error("[WebSocket]: Interrupted");
            }
        };

        WebSocketState.instance.onerror = function(event) {
            console.error("[WebSocket]: Error");
        };
    },

    WebSocketCreateGame: function() {
        WebSocketState.instance.send(JSON.stringify({
            "type": "create-game"
        }));
    },

    WebSocketSaveGame: function(appleCount, snakeLength, gameId) {
        WebSocketState.instance.send(JSON.stringify({
            "type": "collect-apple",
            "payload": {
                "appleCount": appleCount,
                "snakeLength": snakeLength,
                "game_id": gameId
            }
        }));
    },

    WebSocketEndGame: function(gameId) {
        WebSocketState.instance.send(JSON.stringify({
            "type": "end-game",
            "payload": {
                "game_id": gameId
            }
        }));
    },

    WebSocketClose: function() {
        if (!WebSocketState.instance) {
            return;
        }

        WebSocketState.instance.close();
    }
};

autoAddDeps(WebSocketLibrary, '$WebSocketState');
mergeInto(LibraryManager.library, WebSocketLibrary);