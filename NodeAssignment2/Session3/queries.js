const Pool = require('pg').Pool
const pool = new Pool({
  user: 'postgres',
  host: 'localhost',
  database: 'demo',
  password: 'ayushi',
  port: 5432,
})


const getcars = (request, response) => {
  pool.query('select car."name" ,make."name" as MakeName, model."name" as ModelName from car join model on car.modelid = model.id join make on car.makeid = make.id ', (error, results) => {
    if (error) {
      throw error
    }
    response.status(200).json(results.rows)
  })
}


const getcarsById = (request, response) => {
  const id = parseInt(request.params.id)

  pool.query('select car."name" ,make."name" as MakeName, model."name" as ModelName from car join model on car.modelid = model.id join make on car.makeid = make.id  WHERE car.id = $1', [id], (error, results) => {
    if (error) {
      throw error
    }
    response.status(200).json(results.rows)
  })
}
//http://localhost:3000/cars

module.exports = {
  getcars,
  getcarsById
}
