const mysql = require("mysql");

let res;
exports.handler = async (event, context, callback) => {
  const con = mysql.createPool({
  host: "database-1.cu3vqm1aaazutk.ap-south-1.rds.amazonaws.com",
  user: "admin",
  password: "palak",
  port: "3306",
  database: "mysqldatabase_1",
});
  let id = parseInt(event.pathParameters.id);
  let query = `select * from customers where id=${id}`;
  console.log(query);
  con.query(query, function (error, results, fields) {
    if (error) {
      res = {
        statusCode: 400,
        headers: {
          "Access-Control-Allow-Headers": "Content-Type",
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Methods": "OPTIONS,POST,GET",
        },
        body: "Some error",
        isBase64Encoded: false,
      };
      callback(res, null);
    }
    res = {
      statusCode: 200,
      headers: {
        "Access-Control-Allow-Headers": "Content-Type",
        "Access-Control-Allow-Origin": "*",
        "Access-Control-Allow-Methods": "OPTIONS,POST,GET",
      },
      body: JSON.stringify(results[0]),
      isBase64Encoded: false,
    };
    callback(null, res);
    console.log(res);
  });

  return new Promise((resolve, reject) => {
    con.end((err) => {
      if (err) return reject();
      const response = {
        statusCode: 200,
        headers: {
          "Access-Control-Allow-Headers": "Content-Type",
          "Access-Control-Allow-Origin": "*",
          "Access-Control-Allow-Methods": "OPTIONS,POST,GET",
        },
        body: JSON.stringify(res),
      };

      resolve(res);
    });
  });
};
