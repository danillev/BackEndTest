ПО которое получает на вход xml файл, который представляет из себя Натурный лист состава поезда. С его помощью можно отследить какие последние действия были совершены с составом. 
На выход возможно получить данные о текущем состоянии поезда в Json и Excel форматах.

Пример XML файла:
<Root>
  <row>
    <--Номер поезда -->
    <TrainNumber></TrainNumber>
    <--Индекс поезда -->
    <TrainIndexCombined></TrainIndexCombined>
    <--Наименование станции отправления -->
    <FromStationName></FromStationName>
    <--Наименование станции назначения -->
    <ToStationName></ToStationName>
    <--Наименование станции дислокации (текущего местонахождения) -->
    <LastStationName></LastStationName>
    <--Дата и время операции над составом -->
    <WhenLastOperation></WhenLastOperation>
    <--Наименование операции -->
    <LastOperationName></LastOperationName>
    <-- Номер накладной -->
    <InvoiceNum></InvoiceNum>
    <-- Позиция вагона в составе -->
    <PositionInTrain></PositionInTrain>
    <-- Номер вагона -->
    <CarNumber></CarNumber>
    <-- Наименование груза -->
    <FreightEtsngName></FreightEtsngName>
    <-- Общий вес вагона с грузом -->
    <FreightTotalWeightKg></FreightTotalWeightKg>
  </row>
{0…..N}
</Root>
