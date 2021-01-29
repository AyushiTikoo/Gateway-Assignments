const Pool = require('pg').Pool
const pool = new Pool({
  user: 'postgres',
  host: 'localhost',
  database: 'demo',
  password: 'ayushi',
  port: 5432,
})

//GET CARS
const getcars = (request, response) => {
  pool.query('select car."name" ,make."name" as MakeName, model."name" as ModelName from car left join model on car.modelid = model.id left join make on car.makeid = make.id ', (error, results) => {
    if (error) {
      throw error
    }
    response.status(200).json(results.rows)
  })
}

//GET CAR BY ID
const getcarsById = (request, response) => {
  const id = parseInt(request.params.id)

  pool.query('select car."name" ,make."name" as MakeName, model."name" as ModelName from car left join model on car.modelid = model.id left join make on car.makeid = make.id  WHERE car.id = $1', [id], (error, results) => {
    if (error) {
      throw error
    }
    response.status(200).json(results.rows)
  })
}
//INSERT NEW CAR
const createCar = (request, response) => {
  let name  = request.body.name;
  let makeid = request.body.makeid;
  let modelid = request.body.modelid;
  pool.query('select * from car where name = $1',[name], (error, results) => {
      if (error) {
        throw error
      }
      //if car doesn't exsist
      if(!results.rowCount)
      {
        pool.query('select * from make where name = $1',[makeid], (error, reslt1) => {
          if (error) {
            throw error
          }
          pool.query('select * from model where name = $1',[modelid], (error, reslt2) => {
            if (error) {
              throw error
            }
            //if both model and make exsists
            if(reslt1.rowCount!=0 && reslt2.rowCount!=0)
            {
              pool.query(' INSERT INTO car (name, makeid,modelid) VALUES($1,(SELECT id from make WHERE name=$2),(SELECT id from model WHERE name=$3) )', [name, makeid,modelid], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  response.status(201).send(`Car added successfully`)
              })
            }
            //if neither model nor make exsists
            else if(reslt1.rowCount==0 && reslt2.rowCount==0){
              pool.query(' INSERT INTO make(name) VALUES ($1)', [makeid], (error, reslts) => {
                if (error) {
                  throw error
                }
                console.log(`New Make added successfully`)
                pool.query(' INSERT INTO model(name) VALUES ($1)', [modelid], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  console.log(`New Model added successfully`)
                  pool.query(' INSERT INTO car (name, makeid,modelid) VALUES($1,(SELECT id from make WHERE name=$2),(SELECT id from model WHERE name=$3) )', [name, makeid,modelid], (error, reslts) => {
                    if (error) {
                      throw error
                    }
                    response.status(201).send(`Car added successfully`)
                  })
                })
              })
            }
            //if make exists but not model
            else if(reslt1.rowCount!=0 && reslt2.rowCount==0){
              pool.query(' INSERT INTO model(name) VALUES ($1)', [modelid], (error, reslts) => {
                if (error) {
                  throw error
                }
                console.log(`New Model added successfully`)
                pool.query(' INSERT INTO car (name, makeid,modelid) VALUES($1,(SELECT id from make WHERE name=$2),(SELECT id from model WHERE name=$3) )', [name, makeid,modelid], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  response.status(201).send(`Car added successfully`)
                })
              })
            }
            //if make doesn't exsist but model does
            else if(reslt1.rowCount==0 && reslt2.rowCount!=0){
              pool.query(' INSERT INTO make(name) VALUES ($1)', [makeid], (error, reslts) => {
                if (error) {
                  throw error
                }
                console.log(`New Make added successfully`)
                pool.query(' INSERT INTO car (name, makeid,modelid) VALUES($1,(SELECT id from make WHERE name=$2),(SELECT id from model WHERE name=$3) )', [name, makeid,modelid], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  response.status(201).send(`Car added successfully`)
                })
              })
            }
          })
        })
      }
      else{
        response.send(`Car already exsists`)
      }
    })
}
//UPDATE CAR
const updatecar = (request, response) => {
  const id = parseInt(request.params.id)
  let name  = request.body.name;
  let makeid = request.body.makeid;
  let modelid = request.body.modelid;
  pool.query('select * from car where id = $1',[id], (error, results) => {
      if (error) {
        throw error
      }
      //if car exsist
      if(results.rowCount!=0)
      {
        pool.query('select * from make where name = $1',[makeid], (error, reslt1) => {
          if (error) {
            throw error
          }
          pool.query('select * from model where name = $1',[modelid], (error, reslt2) => {
            if (error) {
              throw error
            }
            //if both model and make exsists
            if(reslt1.rowCount!=0 && reslt2.rowCount!=0)
            {
              //UPDATE users SET username = $1 WHERE id = $2
              pool.query(' UPDATE car set name =$1 , makeid =(SELECT id from make WHERE name=$2) ,modelid = (SELECT id from model WHERE name=$3) WHERE id = $4', [name, makeid,modelid,id], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  response.status(201).send(`Car Updated successfully with ID: ${id}`)
              })
            }
            //if neither model nor make exsists
            else if(reslt1.rowCount==0 && reslt2.rowCount==0){
              pool.query(' INSERT INTO make(name) VALUES ($1)', [makeid], (error, reslts) => {
                if (error) {
                  throw error
                }
                console.log(`New Make added successfully`)
                pool.query(' INSERT INTO model(name) VALUES ($1)', [modelid], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  console.log(`New Model added successfully`)
                  pool.query('UPDATE car set name =$1 , makeid =(SELECT id from make WHERE name=$2) ,modelid = (SELECT id from model WHERE name=$3) WHERE id = $4', [name, makeid,modelid,id], (error, reslts) => {
                    if (error) {
                      throw error
                    }
                    response.status(201).send(`Car updated successfully with ID: ${id}`)
                  })
                })
              })
            }
            //if make exists but not model
            else if(reslt1.rowCount!=0 && reslt2.rowCount==0){
              pool.query(' INSERT INTO model(name) VALUES ($1)', [modelid], (error, reslts) => {
                if (error) {
                  throw error
                }
                console.log(`New Model added successfully`)
                pool.query('UPDATE car set name =$1 , makeid =(SELECT id from make WHERE name=$2) ,modelid = (SELECT id from model WHERE name=$3) WHERE id = $4', [name, makeid,modelid,id], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  response.status(201).send(`Car updated successfully with ID: ${id}`)
                })
              })
            }
            //if make doesn't exsist but model does
            else if(reslt1.rowCount==0 && reslt2.rowCount!=0){
              pool.query(' INSERT INTO make(name) VALUES ($1)', [makeid], (error, reslts) => {
                if (error) {
                  throw error
                }
                console.log(`New Make added successfully`)
                pool.query('UPDATE car set name =$1 , makeid =(SELECT id from make WHERE name=$2) ,modelid = (SELECT id from model WHERE name=$3) WHERE id = $4', [name, makeid,modelid,id], (error, reslts) => {
                  if (error) {
                    throw error
                  }
                  response.status(201).send(`Car updated successfully with ID: ${id}`)
                })
              })
            }
          })
        })
      }
      else{
        response.send(`Car doesn't exsists`)
      }
    })
  }
//DELETE CAR
const deleteCar = (request, response) => {
  const id = parseInt(request.params.id)

  pool.query('DELETE FROM car WHERE id = $1', [id], (error, results) => {
    if (error) {
      throw error
    }
    response.status(200).send(`Car deleted with ID: ${id}`)
  })
}
module.exports = {
  getcars,
  getcarsById,
  createCar,
  updatecar,
  deleteCar,
}
