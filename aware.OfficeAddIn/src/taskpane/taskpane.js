/*
 * Copyright (c) Microsoft Corporation. All rights reserved. Licensed under the MIT license.
 * See LICENSE in the project root for license information.
 */

/* global document, Excel, Office */

import { getUserProfile } from "../helpers/sso-helper";
import { filterUserProfileInfo } from "./../helpers/documentHelper";
import jwt_decode from "jwt-decode";
import { data } from "jquery";

async function getIDToken() {
  try {
    const element = document.getElementById("item-subject");
    let userTokenEncoded = await OfficeRuntime.auth.getAccessToken({
      allowSignInPrompt: true,
    });
    let userToken = jwt_decode(userTokenEncoded);
    console.log(userToken);
    element.innerHTML =
      "name: " +
      userToken.name +
      
      "<br>email: " +
      userToken.preferred_username +
      "<br>id: " +
      userToken.name;
  } catch (error) {
    document.getElementById("userInfo").innerHTML =
      "An error occurred. <br>Name: " +
      error.name +
      "<br>Code: " +
      error.code +
      "<br>Message: " +
      error.message;
    console.log(error);
  }
}

async function blockWholeDay() {
  let userTokenEncoded = await OfficeRuntime.auth.getAccessToken({
    allowSignInPrompt: true,
    allowConsentPrompt: true,
    forMSGraphAccess: true,
  });  
  const element = document.getElementById("item-subject");
  element.innerHTML ='Loading request...';
  const headers = { 'Authorization': 'Bearer ' + userTokenEncoded}; // auth header with bearer token
  element.innerHTML = headers.Authorization;
  let path = 'https://localhost:7068/calendar'
  await fetch(path, { headers })
  .then(response => response.json())
  .then(data => element.innerHTML = data.name);
}

async function scheduleShortBreak() {
  let userTokenEncoded = await OfficeRuntime.auth.getAccessToken({
    allowSignInPrompt: true,
    allowConsentPrompt: true,
    forMSGraphAccess: true,
  });  
  const element = document.getElementById("item-subject");
  element.innerHTML ='Loading request...';
  let path = 'https://testapi.jasonwatmore.com/products/1'
  await fetch(path)
  .then(response => response.json())
  .then(data => element.innerHTML = data.name);
}

Office.onReady((info) => {
  if (info.host === Office.HostType.Excel) {
    document.getElementById("getProfileButton").onclick = run;
    document.getElementById("run").onclick = getIDToken;
    document.getElementById("down").onclick = scheduleShortBreak;
    document.getElementById("recharge").onclick = blockWholeDay;
  }
});

export async function run() {
  getUserProfile(writeDataToOfficeDocument);
}

function writeDataToOfficeDocument(result) {
  return Excel.run(function (context) {
    const sheet = context.workbook.worksheets.getActiveWorksheet();
    let data = [];
    let userProfileInfo = filterUserProfileInfo(result);

    for (let i = 0; i < userProfileInfo.length; i++) {
      if (userProfileInfo[i] !== null) {
        let innerArray = [];
        innerArray.push(userProfileInfo[i]);
        data.push(innerArray);
      }
    }
    const rangeAddress = `B5:B${5 + (data.length - 1)}`;
    const range = sheet.getRange(rangeAddress);
    range.values = data;
    range.format.autofitColumns();

    return context.sync();
  });
}
