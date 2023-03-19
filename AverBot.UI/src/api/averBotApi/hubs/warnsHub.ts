import * as signalR from '@microsoft/signalr'

const AVERBOT_API_URI = process.env.AVERBOT_API_URI
const AVERBOT_API_HUB_WARNS = process.env.AVERBOT_API_HUB_WARNS
const token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIiLCJqdGkiOiJkYjI0ZDI5Zi03NzQ2LTQ1N2MtYjRlNC1kNDZiZDljOWRjNDMiLCJuYmYiOjE2NzkyNDIwNDgsImV4cCI6MTY3OTU4NzQ3OCwiaWF0IjoxNjc5MjQyMDQ4LCJpc3MiOiJodHRwczovL2F2ZXJsaXN0Lnh5ei8iLCJhdWQiOiJodHRwczovL2F2ZXJsaXN0Lnh5ei8ifQ.JR5qXhhz12Hs5Fb80BkTsYpY3Eiy8fxQfdr1n5I9z4xrpAtVSv6y3A5BwOu44VZWIHeWcnIqT1W7kpTeDFRlpg"

export const warnsHubConnection = new signalR.HubConnectionBuilder()
    .withUrl(`${AVERBOT_API_URI}${AVERBOT_API_HUB_WARNS}?access_token=${token}`)
    .build()
