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
