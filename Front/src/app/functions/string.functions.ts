export function isNumeric(val: any) {
  return !isNaN(parseFloat(val)) && isFinite(val);
}

export function isFunction(fn: any) {
  return typeof fn === 'function';
}

export function stringIsNullOrWhiteSpace(value: string) {
  return value == undefined || value == null || value.trim() === '';
}

export function parseArrayToURLString(array: any[], parameterName: string, trimFirstAnd: boolean = false) {
  let stringifiedArray = '';

  for (let item of array) {
    stringifiedArray += '&' + parameterName + '=' + item;
  }

  if (trimFirstAnd && stringifiedArray.length > 1) {
    stringifiedArray = stringifiedArray.substr(1, stringifiedArray.length);
  }

  return stringifiedArray;
}

export function isEmpty(obj: any) {
  return JSON.stringify(obj) === JSON.stringify({});
}

export function validateEmail(email: string) {
  var re = /\S+@\S+\.\S+/;
  return re.test(email);
}

export function isValidImage(img: any, defaultImg: string) {
  return img && img.trim() !== '' && img.trim() !== defaultImg.trim();
}

export function base64ToBlob(b64Data: string, contentType = '', sliceSize = 512) {
  b64Data = b64Data.replace(/\s/g, ''); //IE compatibility...
  let byteCharacters = atob(b64Data);
  let byteArrays = [];
  for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
    let slice = byteCharacters.slice(offset, offset + sliceSize);

    let byteNumbers = new Array(slice.length);
    for (var i = 0; i < slice.length; i++) {
      byteNumbers[i] = slice.charCodeAt(i);
    }
    let byteArray = new Uint8Array(byteNumbers);
    byteArrays.push(byteArray);
  }
  return new Blob(byteArrays, { type: contentType });
}
