const mysql = require("mysql");
const con = mysql.createPool({
  host: "database-1.cu3vqm1aaazutk.ap-south-1.rds.amazonaws.com",
  user: "admin",
  password: "palak",
  port: "3306",
  database: "mysqldatabase_1",
});
let res;

exports.handler = async (event) => {
  return new Promise((resolve, reject) => {
    con.getConnection(function (error, connection) {
      if (error) throw error;
      let sqlQuery = "select * from customers;";
      return connection.query(sqlQuery, (err, rows, fields) => {
        if (error) {
          throw error;
        } else {
          connection.release();
          return resolve(rows);
        }
      });
    });
  });
};
