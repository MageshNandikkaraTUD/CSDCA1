import { check, sleep } from "k6";
import http from "k6/http";

export let options = {

  stages: [
    { duration: "1m", target: 20 },            // 1 new vu every 3 seconds
    { duration: "1m", target: 20 },
    { duration: "1m", target: 0 }             // 1 less vu every 3 seconds
  ],
  
 	thresholds: {
    "http_req_duration": ["p(95) < 300"]
  },

  discardResponseBodies: false,
  
  cloud: {
    distribution: {
      distributionLabel1: { loadZone: 'amazon:us:ashburn', percent: 50 },
      distributionLabel2: { loadZone: 'amazon:ie:dublin', percent: 50 },
    },
  },
 
};

/**
 * Get a random integer between `min` and `max`.
 * 
 * @param {number} min - min number
 * @param {number} max - max number
 * @return {number} a random integer
 */

function getRandomInt(min, max) {
  return Math.floor(Math.random() * (max - min + 1) + min);
}

export default function() {
 
  let res = http.get("https://mn-bpcalculator-staging.azurewebsites.net/", {"responseType": "text"});

  check(res, {
    "is status 200": (r) => r.status === 200
  });
  res = res.submitForm({
    fields: { BP_Systolic: getRandomInt(70, 190).toString(), BP_Diastolic: getRandomInt(40, 100).toString()}
  });

  check(res, {
    "is status 200": (r) => r.status === 200
  });

  sleep(3);
}
