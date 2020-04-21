var timeout = 1585715421022;
var access_token = ""

function getTime() {
    return new Date().getTime();
}

// Call without arguments ArcGisAPIController(foo)
// Call with arguments ArcGisAPIController(foo, ["Hello ", "World!"])
async function ArcGisAPIController(func, args) {
    var token
    creditials = await getCreditials();
    token = await getToken(creditials);
    tokenArgs = [token].concat(args);
    func.apply(this, tokenArgs)
}

// Call using a async function like controller
// so the await can be used, otherwise the
// token won't be ready by the time you make a call
function getToken(creditials) {

    // Get creditiionals from going to either https://developers.arcgis.com/dashboard or https://developers.arcgis.com/applications and creating an application
    // Need to switch these out perferrable with a stored value for each user in appsettings.json, but for the project these can be manually changed
    client_id = creditials.client_id;
    client_secret = "";

    grant_type = "client_credentials";

    var time = getTime();

    if (time > timeout) {
        var options = {
            method: 'POST',
            url: 'https://www.arcgis.com/sharing/rest/oauth2/token',
            headers:
                { 'content-type': 'application/x-www-form-urlencoded' },
            data:
            {
                client_id: creditials.client_id,
                client_secret: creditials.client_secret,
                grant_type: creditials.grant_type
            }
        };

        return new Promise(function (resolve, reject) {
            $.ajax(options).done(function (response) {
                // returns the access token response - even if the call fails to connect
                var msg = JSON.parse(response);
                access_token = msg.access_token;
                timeout = time + (msg.expires_in * 1000);
                resolve(access_token)
            })
                .fail(function (xhr, status, error) {
                    console.log("Error in getToken: " + error);
                });
        })
    } else {
        return access_token
    }
};

function getCreditials() {
    return new Promise(function (resolve, reject) {
    $.ajax({
        url: "/Home/GetAccessTokenCred",
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        success: function (data, status, xhr) {
            resolve(data)
        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            alert(err.Message);
        }
    })
    });
}