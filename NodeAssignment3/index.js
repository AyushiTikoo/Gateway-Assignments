const express = require('express')
const bodyParser = require('body-parser')
const db = require('./queries')

const app = express()
const port = 3000

app.use(bodyParser.json())
app.use(
  bodyParser.urlencoded({
    extended: true,
  })
)

app.get('/', (request, response) => {
  response.json({ info: 'Assignment-3' })
})
app.get('/cars', db.getcars)
app.get('/cars/:id', db.getcarsById)
app.post('/cars', db.createCar)
app.put('/cars/:id', db.updatecar)
app.delete('/cars/:id', db.deleteCar)

app.listen(port, () => {
  console.log(`App running on port ${port}.`)
})
