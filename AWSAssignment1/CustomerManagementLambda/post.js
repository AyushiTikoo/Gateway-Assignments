const mysql = require("mysql");
const parser = require("lambda-multipart-parser");

const AWS = require("aws-sdk");
const bcrypt = require("bcryptjs");
const ID = "***************";
const SECRET = "*************************";
const s3 = new AWS.S3({
  accessKeyId: ID,
  secretAccessKey: SECRET,
});
const con = mysql.createPool({
  host: "database-1.cu3vqm1azutk.ap-south-1.rds.amazonaws.com",
  user: "admin",
  password: "palakagrawal",
  port: "3306",
  database: "mysqldatabase_1",
});

function uploadFile(filePath, content) {
  return new Promise((resolve, reject) => {
    const params = {
      ACL: "public-read",
      Bucket: "customermanagementsystem",
      Key: filePath,
      Body: content,
    };

    s3.upload(params, function (err, data) {
      console.log("IN UPLOAD");
      console.log("DATA" + data);
      console.log("DATA" + data.Location);
      if (err) {
        throw err;
      }
      let path = data.Location;
      console.log(`File uploaded successfully. ${data.Location}`);
      resolve(path);
    });
  });
}

exports.handler = async (event) => {
  return new Promise(async (resolve, reject) => {
    const result = await parser.parse(event);
    var filePath = "customers/profile/" + result.name + Date.now() + ".jpg";
    let path = await uploadFile(filePath, result.files[0].content);
    console.log("RESULT   " + result);

    var password = result.password;
    const passwordHash = bcrypt.hashSync(password, 8);
    console.log(passwordHash);

    con.getConnection((err, connection) => {
      console.log("IN");
      connection.query("use customermanagementsystem");
      if (err) console.log(err);
      return connection.query(
        `Insert into customers(name,email,password,contactNo,image,status) values(${con.escape(
          result.name
        )},${con.escape(result.email)},${con.escape(passwordHash)},${con.escape(
          result.contactNo
        )},${con.escape(path)},${con.escape(result.status)})`,
        function (err, result) {
          if (err)
            return resolve({
              statusCode: 200,
              headers: {
                "Access-Control-Allow-Headers": "Special-Request-Header",
                "Access-Control-Allow-Origin": "*",
                "Access-Control-Allow-Methods": "OPTIONS,POST,GET",
                "Access-Control-Allow-Credentials": true,
              },
              body: JSON.stringify("This email already exists"),
            });

          return resolve({
            statusCode: 200,
            headers: {
              "Access-Control-Allow-Headers": "Special-Request-Header",
              "Access-Control-Allow-Origin": "*",
              "Access-Control-Allow-Methods": "OPTIONS,POST,GET",
              "Access-Control-Allow-Credentials": true,
            },
            body: JSON.stringify("Data inserted successfully"),
          });
        }
      );
    });
  });
};
