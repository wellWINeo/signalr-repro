const signalr = require('@microsoft/signalr');
const process = require('process');

const URL = 'http://localhost:5047/hub';
const ACCESS_TOKEN = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InVzcGVuIiwic3ViIjoidXNwZW4iLCJqdGkiOiJiZDE2MWVmMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTA0NyIsIm5iZiI6MTcwODQyMjI4NCwiZXhwIjoxNzE2MTk4Mjg0LCJpYXQiOjE3MDg0MjIyODUsImlzcyI6ImRvdG5ldC11c2VyLWp3dHMifQ.4thMRYcPQHwIw1ou3G3iJ1CgGk6j0Atj6oynrfwgsu8';

const connection = new signalr.HubConnectionBuilder()
    .withUrl(URL, {
        transport: signalr.HttpTransportType.WebSockets,
        accessTokenFactory: () => ACCESS_TOKEN,
    })
    .build();

connection
    .start()
    .then(() => {
        connection
            .invoke('getName')
            .then(console.log)
            .then(process.exit)
    });