# ECSS-E-TM-10-25-Annex-B-Validator

The purpose of the **ECSS-E-TM-10-25-Annex-B-Validator** is to validate the reference data contained in a [ECSS-E-TM-10-25 Annex-C3](https://github.com/RHEAGROUP/CDP4-SDK-Community-Edition/wiki/Annex-C#json-exchange-file-format) archive. A publicly available Annex B is availble at the following URI https://github.com/RHEAGROUP/ECSS-E-TM-10-25-Annex-B which is validated using this software. The data is validated along 2 axis:
  - Annex A correctness
  - User Defined validation rules

The [CDP4-SDK](https://github.com/RHEAGROUP/CDP4-SDK-Community-Edition) is used to develop the validator.

## ECSS-E-TM-10-25A

ECSS-E-TM-10-25A, under the ECSS-E-10 System Engineering in the engineering branch of ECSS series of documents, defines the recommendations for model based data exchange for the early phases of engineering design.

This Technical Memorandum facilitates and promotes common data definitions and exchange among partner Agencies, European space industry and institutes, which are interested to collaborate on concurrent design, sharing analysis and design outputs and related reviews. This comprises a decomposition of a system down to equipment level and related defined lists of parameters and disciplines. Further it provides the starting point of the space system life cycle defining the parameter sets needed to cover all project phases, although the present Technical Memorandum only addresses Phases 0 and A. **Concurrent Design, based on ECSS-E-TM-10-25, has been applied in industrial contexts up to phase B1, including the SRR.**

> Even though ECSS-E-TM-10-25 has been developed in the context of space systems engineering, it is by no means limited to this industry. Concurrent Design and its application based on ECSS-E-TM-10-25 has been successfully implemented in many other industries such as: Commercial Aircraft, Building and Construction, Shipbuilding, Oil and Gas, Food Industry.

When viewed in a specific project context, the information provided in this Technical Memorandum should be adapted to match the specific need of a particular profile and circumstances of a project. Ultimately, the objectives of the work can be summarised in three points:
* Creation of concurrent design facilities using a common / compatible data model;
* Enable effective data transfer across models belonging to different but compatible organizations / facilities;
* Enable real-time collaboration and joint activities among multiple organizations / facilities.

ECSS-E-TM-10-25 contains 3 annexes that can be used to create software implementations:
* [Annex A](https://github.com/RHEAGROUP/CDP4-SDK-Community-Edition/wiki/ECSS-E-TM-10-25A-Annex-A): The formal definition of the _Space_ Engineering Information Model (SEIM).
* [Annex B](https://github.com/RHEAGROUP/ECSS-E-TM-10-25-Annex-B): The formal definition of the Space Engineering Reference Data Library (SERDL).
* [Annex C](https://github.com/RHEAGROUP/CDP4-SDK-Community-Edition/wiki/Annex-C): The formal definition of the **Web Services REST API**, and the **JSON Exchange File Format**

> parts of this text have been copied from the ECSS website and can be found [here](http://ecss.nl/hbstms/ecss-e-tm-10-25a-engineering-design-model-data-exchange-cdf-20-october-2010/);