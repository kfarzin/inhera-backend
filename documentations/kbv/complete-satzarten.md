# LDT Protocol - Complete Satzarten (Record Types)

## Overview

The LDT (Labordatenträger) protocol defines multiple record types (Satzarten) for exchanging laboratory data between medical practices and laboratories in the German healthcare system.

---

## Complete List of Satzarten

### Infrastructure/Control Records

These records manage the data package structure and are used for file organization.

| Satzart | Name | Description |
|---------|------|-------------|
| **0020** | Datenträger-Header | Media Header - First record on each physical media |
| **0021** | Datenträger-Abschluss | Media Footer - Last record on each physical media |
| **8220** | L-Datenpaket-Header | Lab Data Package Header |
| **8221** | L-Datenpaket-Abschluss | Lab Data Package Footer |
| **8230** | P-Datenpaket-Header | Practice Data Package Header |
| **8231** | P-Datenpaket-Abschluss | Practice Data Package Footer |

**Note:** Records 0020 and 0021 are only required when data must be split across multiple physical media (e.g., diskettes).

---
Labordatenträger
## Report/Data Exchange Records

### Incoming Reports (Labs → Practices)

These record types represent laboratory results being sent to medical practices.

#### 8201 - Labor-Bericht (Laboratory Report)
- **Direction:** Laborfacharzt → Arztpraxis
- **Purpose:** Standard laboratory test results
- **Description:** Primary report type for general laboratory test results from lab specialists to medical practices

#### 8202 - LG-Bericht (Laboratory Community Report)
- **Direction:** Laborgemeinschaft → Arztpraxis
- **Purpose:** Lab results from community-operated laboratories
- **Description:** Reports from laboratory communities (Laborgemeinschaft) - collaborations of multiple physicians operating a shared laboratory

#### 8203 - Mikrobiologie-Bericht (Microbiology Report)
- **Direction:** Laborfacharzt → Arztpraxis
- **Purpose:** Specialized microbiology laboratory results
- **Description:** Specialized reports for microbiological testing and analysis

#### 8204 - Labor-Bericht "Sonstige Einsendepraxen"
- **Direction:** Sonstige Einsendepraxen → Arztpraxis
- **Purpose:** Laboratory reports from other sending practices
- **Description:** Reports from other specialized practices (e.g., pathologists) that perform laboratory testing on submitted samples

---

### Outgoing Orders (Practices → Labs)

These record types represent test orders/referrals being sent from practices to laboratories.

#### 8218 - Elektronische Überweisung (Electronic Referral)
- **Direction:** Arztpraxis → Laborfacharzt
- **Purpose:** Laboratory test orders/requests
- **Description:** Electronic referral/order for laboratory tests from medical practice to lab specialist

#### 8219 - Auftrag an eine Laborgemeinschaft (Laboratory Community Order)
- **Direction:** Arztpraxis → Laborgemeinschaft
- **Purpose:** Test orders for laboratory communities
- **Description:** Orders specifically directed to laboratory communities using form Muster 10A or electronic referral (SA 8218) with Scheinuntergruppe 28

---

## Data Exchange Flows

### 1. Practice to Lab Specialist
```
Arztpraxis --[SA 8218]--> Laborfacharzt
Laborfacharzt --[SA 8201 or SA 8203]--> Arztpraxis
```

### 2. Practice to Laboratory Community
```
Arztpraxis --[SA 8219]--> Laborgemeinschaft
Laborgemeinschaft --[SA 8202]--> Arztpraxis
```

### 3. Other Sending Practice to Practice
```
Sonstige Einsendepraxen --[SA 8204]--> Arztpraxis
```

---

## Summary by Type

### Reports (4 types)
- **8201** - Standard Lab Report
- **8202** - Lab Community Report
- **8203** - Microbiology Report
- **8204** - Other Sending Practice Report

### Orders (2 types)
- **8218** - Electronic Referral
- **8219** - Lab Community Order

### Infrastructure (6 types)
- **0020, 0021** - Media-level headers/footers
- **8220, 8221** - Lab package headers/footers
- **8230, 8231** - Practice package headers/footers

---

## Key Concepts

### Arztpraxis (Medical Practice)
Practice of a physician participating in contracted medical care with patient contact.

### Laborfacharzt (Lab Specialist)
Specialist laboratory physician conducting tests on submitted samples.

### Laborgemeinschaft (Laboratory Community)
Collaboration of multiple practicing physicians jointly operating a laboratory where samples from members are tested. Since October 1, 2008, laboratory communities bill their services directly with the health insurance association (KV).

### Einsendepraxis (Sending Practice)
Practice where patients typically do not visit directly, but rather the medical services are based on examination of submitted bodily materials. Includes:
- Laborfacharzt (lab specialists)
- Sonstige Einsendepraxen (other sending practices, e.g., pathologists)
- Laborgemeinschaft (laboratory communities)

---

## File Structure Requirements

Each LDT file must:
1. Use only permitted Satzart combinations
2. Be generated and read separately
3. Optionally be packaged (compressed) with other LDT files for physical transmission
4. Follow specific sequencing rules based on the transmission method (e.g., DFÜ-Verfahren)

---

## Reference
Source: KBV_ITA_VGEX_Datensatzbeschreibung_LDT-1.htm
Version: LDT1014.01
