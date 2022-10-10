const createProxyMiddleware = require('http-proxy-middleware');
const { env } = require('process');

const target = 'http://localhost:44464';

const context =  [
  "topten",
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};

/* 
 
 const createProxyMiddleware = require('http-proxy-middleware');
const { env } = require('process');

//const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
//  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:64974';

const context =  [
  "api/tweet/topten",
];

module.exports = function(app) {
  const appProxy = createProxyMiddleware(context, {
      target: 'https://localhost:7044/api', //target, https://localhost:7044/
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  });

  app.use(appProxy);
};

 
 */